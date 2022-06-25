using Amazon;
using Amazon.S3.Model;
using ImageLabeller.DbModels;
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
    public async Task<List<SourceImage>> Get()
    {
        return await _imagesRepository.GetImageNames();
    }
    
    [HttpGet("next-image-info")]
    public async Task<ImageDetailsResponse> GetNextImage(int currentIndex)
    {
        var imageDetails = await _imagesRepository.GetImageAtIndex(++currentIndex);

        var imageLabels = await _labelsRepository.GetLabels(imageDetails.ImageId);

        return new ImageDetailsResponse()
        {
            SourceImage = imageDetails,
            ImageLabels = imageLabels
        };
    }

    [HttpGet("render-image-from-s3")]
    public async Task<FileContentResult> RenderImageFromS3(Guid id)
    {
        var imagedetails = await _imagesRepository.GetImageById(id);

        var image = await _imagesRepository.GetCachesS3File(imagedetails.S3Path);
        
        return File(image, "image/jpeg");
    }
    
    [HttpGet("render-image-from-file")]
    public async Task<FileContentResult> RenderImageFromFile(int index)
    {
        var images = await _imagesRepository.GetImageNames();

        var imagedetails = images[index];

        var imageBytes = System.IO.File.ReadAllBytes(imagedetails.LocalFilePath);  
        
        return File(imageBytes, "image/jpeg");
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