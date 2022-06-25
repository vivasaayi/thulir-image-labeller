using ImageLabeller.DbModels;
using ImageLabeller.Repositories;
using ImageLabeller.WebModels;
using Microsoft.AspNetCore.Mvc;

namespace ImageLabeller.Controllers;

[ApiController]
[Route("[controller]")]
public class LabelsController : ControllerBase
{
    private LabelsRepository _labelsRepository = new LabelsRepository();
    
    [HttpGet]
    public async Task<ImageLabels> Get(string? imageId)
    {
        return _labelsRepository.GetLables(imageId);
    }
    
    [HttpPost]
    public async Task<ImageLabels> Post(int imageId)
    {
        var imageLables = new ImageLabels()
        {
            Image = new SourceImage()
            {
                ImageIndex = imageId,
            }
        };
        
        _labelsRepository.SaveLabels(imageLables);

        return imageLables;
    }
}