using Amazon;
using Amazon.Internal;
using ImageLabeller.Dals;
using ImageLabeller.DbModels;
using ImageLabeller.Services;
using Npgsql;

namespace ImageLabeller.Repositories;

public class ImagesRepository
{
    private string[] _fileNames;
    private bool _filesLoaded;
    private string _lockObject = "lock";

    private string _path =
        "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1";

    private PostgresDal _dal;
    private static List<SourceImage> _sourceImages = new List<SourceImage>();
    private static Dictionary<int, SourceImage> _imagesByIndex = new Dictionary<int, SourceImage>();
    private static Dictionary<Guid, SourceImage> _imagesByImageId = new Dictionary<Guid, SourceImage>();

    private Dictionary<Guid, string> _presignedUrls = new Dictionary<Guid, string>();
    private S3Client _client = new S3Client(RegionEndpoint.USWest2);

    public ImagesRepository()
    {
        _dal = PostgresDal.GetInstance();
    }

    private async Task LoadImagesListFromDatabase()
    {
        if (_sourceImages.Count > 0)
        {
            return;
        }

        try
        {
            string command = @"SELECT * FROM sourceimages";

            var result = await _dal.ExecuteQuery<SourceImage>(command, new { });

            lock (_lockObject)
            {
                _sourceImages = result.ToList();

                _imagesByIndex = new Dictionary<int, SourceImage>();
                _imagesByImageId = new Dictionary<Guid, SourceImage>();

                foreach (var sourceImage in _sourceImages)
                {
                    if (sourceImage.S3Path.EndsWith(".jpg"))
                    {
                        var startIndex = sourceImage.S3Path.LastIndexOf("/");
                        sourceImage.ImageName = sourceImage.S3Path.Substring(startIndex,
                            sourceImage.S3Path.Length - startIndex);    
                    }
                    
                    _imagesByIndex.Add(sourceImage.ImageIndex, sourceImage);
                    _imagesByImageId.Add(sourceImage.ImageId, sourceImage);
                }
            }
        }
        catch (PostgresException err)
        {
            Console.WriteLine(err);
        }
    }

    public async Task<SourceImage> GetImageAtIndex(int index)
    {
        await LoadImagesListFromDatabase();

        return _imagesByIndex[index];
    }

    public async Task<SourceImage> GetImageById(Guid imageId)
    {
        await LoadImagesListFromDatabase();

        return _imagesByImageId[imageId];
    }

    public async Task<List<SourceImage>> GetImageNames()
    {
        await LoadImagesListFromDatabase();
        return _sourceImages;
    }

    async Task<Stream> DownloadFile(string key)
    {
        var result = await _client.GetObjectStream("myagridataset", key);
        return result;
    }

    public async Task<byte[]> GetCachesS3File(string s3Key)
    {
        var basePath = "/Users/rajanp/datasets_local/";
        var localFilePath = basePath + s3Key;

        if (File.Exists(localFilePath))
        {
            return await System.IO.File.ReadAllBytesAsync(localFilePath);
        }

        var imageFolder = localFilePath.Substring(0, localFilePath.LastIndexOf("/"));

        if (!Directory.Exists(imageFolder))
        {
            Directory.CreateDirectory(imageFolder);
        }

        var stream = await DownloadFile(s3Key);
        var imageBytes = new BinaryReader(stream).ReadBytes((int) stream.Length);
        await File.WriteAllBytesAsync(localFilePath, imageBytes);

        return imageBytes;
    }

    public void GeneratePresignedUrl(SourceImage image)
    {
        if (!_presignedUrls.ContainsKey(image.ImageId))
        {
            var preSignedUrl = _client.GeneratePresignedUrl("myagridataset", image.S3Path);
            image.PreSignedUrl = preSignedUrl;

            lock (_lockObject)
            {
                if (!_presignedUrls.ContainsKey(image.ImageId))
                {
                    _presignedUrls.Add(image.ImageId, preSignedUrl);
                }
            }
        }
    }
}