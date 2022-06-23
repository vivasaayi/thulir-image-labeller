using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ImageLabeller.Dals;
using ImageLabeller.DbModels;
using ImageLabeller.Models;
using ImageLabeller.Utilities;
using Npgsql;

namespace ImageLabeller.Services;

public class DataSetSyncService
{
    private static readonly RegionEndpoint _bucketRegion = RegionEndpoint.USWest2;
    private PostgresDal _dal;

    public DataSetSyncService()
    {
        _dal = PostgresDal.GetInstance();
    }

    public async Task<S3SyncResult> SyncDataSetToDatabase(string s3Key)
    {
        var files = await GetFileNamesFromS3(s3Key);
        var syncResult = await WriteFilesToDatabase(files);
        Console.WriteLine("Files written to database");
        syncResult.S3Key = s3Key;

        return syncResult;
    }

    public async Task<S3SyncResult> WriteFilesToDatabase(List<S3Object> files)
    {
        int success = 0;
        int failed = 0;
        foreach (var file in files)
        {
            try
            {
                string command = @"INSERT INTO sourceimages (imageid, s3path, indexedtime)
                               VALUES (@imageId, @s3Path, @indexedTime)";
            
                await _dal.InsertRecord(command, new 
                {
                    imageId = Guid.NewGuid(),
                    s3Path = file.Key,
                    indexedTime = DateTime.Now
                });

                success++;
            }
            catch (PostgresException err)
            {
                Console.WriteLine(err);
                failed++;
            }
        }
        
        return new S3SyncResult() {
            NumberOfRecords = files.Count, 
            Success = success, 
            Failed = failed
        };
    }

    public async Task<List<S3Object>> GetFileNamesFromS3(string s3Key)
    {
        var s3Client = new S3Client(_bucketRegion);
        var globals = await ConfigLoader.GetInstance().GetGlobals();
        var files = await s3Client.GetFileNames(
            globals.RawDataSetPath, 
            RequestPayer.Requester,
            s3Key);
        return files;
    }
}