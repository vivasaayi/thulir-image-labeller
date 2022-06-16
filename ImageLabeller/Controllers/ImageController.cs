using ImageLabeller.Repositories;
using ImageLabeller.WebModels;
using Microsoft.AspNetCore.Mvc;

namespace ImageLabeller.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private LabelsRepository _labelsRepository = new LabelsRepository();
    private ImagesRepository _imagesRepository = new ImagesRepository();
    
    [HttpGet]
    public async Task<List<Image>> Get()
    {
        return _imagesRepository.GetImageNames();
    }
    
    [HttpGet("next-image-info")]
    public async Task<Image> GetNextImage(int index)
    {
        var images = _imagesRepository.GetImageNames();

        return images[index++];
    }
    
    [HttpGet("render-image")]
    public async Task<FileContentResult> RenderImage(int index)
    {
        var images = _imagesRepository.GetImageNames();

        var imagedetails = images[index++];

        var imageBytes = System.IO.File.ReadAllBytes(imagedetails.ImageLocation);  
        
        return File(imageBytes, "image/jpeg");
    }
    
    [HttpPost]
    public async Task<ImageLabels> Post(int imageId)
    {
        var imageLables = new ImageLabels()
        {
            Image = new Image()
            {
                ImageId = imageId,
                ImageName = "ABC",
                ImageLocation = "cotton.jpg"
            }
        };
        
        _labelsRepository.SaveLabels(imageLables);

        return imageLables;
    }
}