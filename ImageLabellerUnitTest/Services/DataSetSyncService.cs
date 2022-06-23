using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Dapper;
using ImageLabeller.Dals;
using ImageLabeller.Models;
using ImageLabeller.Services;
using ImageLabeller.Utilities;
using NUnit.Framework;

namespace ImageLabellerUnitTest.Services;

public class DataSetSyncServiceTest
{
    [SetUp]
    public async Task Setup()
    {
        ImageLabellerGlobals globals = await ConfigLoader.GetInstance().GetGlobals();
        PostgresDal.Init(new PostgresConfig(
            globals.PostgresHost,
            globals.PostgresUserName,
            globals.PostgresPassword,
            globals.PostgresDatabase)
        );

        PostgresDal.GetConnection().Query("truncate table  public.sourceimages");
    }
    [Test]
    public async Task Test1()
    {
        var syncService = new DataSetSyncService();
        var files = new List<S3Object>();
        files.Add(new S3Object()
        {
            Key = "AAA"
        });
        files.Add(new S3Object()
        {
            Key = "BBB"
        });
        files.Add(new S3Object()
        {
            Key = "CCC"
        });
        files.Add(new S3Object()
        {
            Key = "AAA"
        });
        
        await syncService.WriteFilesToDatabase(files);
        Assert.AreEqual(1, 1);
    }

    //await syncService.SyncDataSetToDatabase();   
}