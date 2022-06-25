using Amazon;
using Amazon.Internal;
using ImageLabeller.Dals;
using ImageLabeller.DbModels;
using ImageLabeller.Services;
using ImageLabeller.Utilities;
using ImageLabeller.WebModels;
using Npgsql;

namespace ImageLabeller.Repositories;

public class ImagesRepository
{
    private string[] _fileNames;
    private bool _filesLoaded;
    private string _lockObject = "lock";
    private string _path = "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1";
    
    private PostgresDal _dal;
    private List<SourceImage> _images = new List<SourceImage>();

    private Dictionary<Guid, string> _presignedUrls = new Dictionary<Guid, string>();
    private S3Client _client = new S3Client(RegionEndpoint.USWest2);

    public ImagesRepository()
    {
        _dal = PostgresDal.GetInstance();
    }

    private async Task LoadFileNames()
    {
        if (_images.Count > 0)
        {
            return;
        }
        
        try
        {
            string command = @"SELECT * FROM sourceimages";
            
            var result = await _dal.ExecuteQuery<SourceImage>(command, new {});

            var imageId = 0;
            foreach (var record in result)
            {
                record.RowNumber = imageId++; 
                _images.Add(record);
            }
            
        }
        catch (PostgresException err)
        {
            Console.WriteLine(err);
        }
    }

    public async Task<Image> GetImageAtIndex(int index)
    {
        var imageNames = await GetImageNames();

        var image = imageNames[index];

        // if (!_presignedUrls.ContainsKey(image.ImageId))
        // {
        //     var preSignedUrl = _client.GeneratePresignedUrl("myagridataset", image.ImageLocation);
        //     image.PreSignedUrl = preSignedUrl;
        //     
        //     lock (_lockObject)
        //     {
        //         if (!_presignedUrls.ContainsKey(image.ImageId))
        //         {
        //             _presignedUrls.Add(image.ImageId, preSignedUrl);
        //         }
        //     }
        // }
        
        return image;
    }
    
    public async Task<List<Image>> GetImageNames()
    {
        await LoadFileNames();
        
        var result = new List<Image>();
        
        foreach (var sourceImage in _images)
        {
            string[] split = sourceImage.S3Path.Split("/");
            var imageName = split[split.Length - 1];
            var image = new Image()
            {
                ImageIndex = sourceImage.RowNumber,
                ImageId = sourceImage.ImageId,
                ImageName = imageName,
                ImageLocation = sourceImage.S3Path
            };

            result.Add(image);
        }

        return result;
    }

    public async Task<Stream> DownloadFile(string key)
    {
        var result = await _client.GetObjectStream("myagridataset", key);
        return result;
    }
}