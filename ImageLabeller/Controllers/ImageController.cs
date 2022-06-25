using Amazon;
using Amazon.S3.Model;
using ImageLabeller.Models;
using ImageLabeller.Repositories;
using ImageLabeller.Services;
using ImageLabeller.WebModels;
using Microsoft.AspNetCore.Mvc;

namespace ImageLabeller.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private LabelsRepository _labelsRepository = new LabelsRepository();
    private ImagesRepository _imagesRepository = new ImagesRepository();
    private DataSetSyncService _dataSetSyncService = new DataSetSyncService();
    
    
    [HttpGet]
    public async Task<List<Image>> Get()
    {
        return await _imagesRepository.GetImageNames();
    }
    
    [HttpGet("next-image-info")]
    public async Task<Image> GetNextImage(int index)
    {
        var imageDetails = await _imagesRepository.GetImageAtIndex(++index);

        return imageDetails;
    }

    [HttpGet("render-image-from-s3")]
    public async Task<FileContentResult> RenderImageFromS3(int index)
    {
        var images = await _imagesRepository.GetImageNames();

        var imagedetails = images[index];

        var stream = await _imagesRepository.DownloadFile(imagedetails.ImageLocation);
        
        var imageBytes = new BinaryReader(stream).ReadBytes((int)stream.Length);  
        
        return File(imageBytes, "image/jpeg");
    }
    
    [HttpGet("render-image-from-file")]
    public async Task<FileContentResult> RenderImageFromFile(int index)
    {
        var images = await _imagesRepository.GetImageNames();

        var imagedetails = images[index];

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
                ImageIndex = imageId,
                ImageName = "ABC",
                ImageLocation = "cotton.jpg"
            }
        };
        
        _labelsRepository.SaveLabels(imageLables);

        return imageLables;
    }
    
    [HttpPost("sync-files-from-s3")]
    public async Task<IActionResult> SyncFilesFromS3(string? s3key)
    {
        if (string.IsNullOrEmpty(s3key))
        {
            return Problem("Specify S3 Key");
        }

        var result = await _dataSetSyncService.SyncDataSetToDatabase(s3key);

        return new JsonResult(result);
    }
}