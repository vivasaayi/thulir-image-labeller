using ImageLabeller.Utilities;
using ImageLabeller.WebModels;

namespace ImageLabeller.Repositories;

public class ImagesRepository
{
    private string[] _fileNames;
    private bool _filesLoaded;
    private string _lockObject = "lock";
    private string _path = "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1"; 

    private void LoadFileNames()
    {
        if (_filesLoaded)
        {
            return;
        }
        lock (_lockObject)
        {
            _fileNames = FileUtils.GetFilesFromFolder(_path);
            _filesLoaded = true;    
        }
    }
    
    public List<Image> GetImageNames()
    {
        LoadFileNames();
        
        var result = new List<Image>();

        var imageId = 0;
        foreach (var fileName in _fileNames)
        {
            string[] split = fileName.Split("/");
            var imageName = split[split.Length - 1];
            var image = new Image()
            {
                ImageId = imageId,
                ImageName = imageName,
                ImageLocation = fileName
            };

            imageId++;

            result.Add(image);
        }

        return result;
    }
}