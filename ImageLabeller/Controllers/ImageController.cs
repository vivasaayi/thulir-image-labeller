using ImageLabeller.Repositories;
using ImageLabeller.WebModels;
using Microsoft.AspNetCore.Mvc;

namespace ImageLabeller.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private LabelsRepository _labelsRepository = new LabelsRepository();
    
    [HttpGet]
    public async Task<string> Get()
    {
        return "new string 4";
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