using System.Text.Json.Serialization;

namespace ImageLabeller.DbModels;

public class SourceImage
{
    [JsonPropertyName("imageId")] 
    public Guid ImageId { get; set; }

    [JsonPropertyName("s3path")] 
    public string S3Path { get; set; }
    
    [JsonPropertyName("imageName")] 
    public string ImageName { get; set; }

    [JsonPropertyName("indexedtime")] 
    public DateTime IndexedTime { get; set; }
    
    [JsonPropertyName("imageIndex")] 
    public int ImageIndex { get; set; }
    
    [JsonPropertyName("localfilepath")] 
    public string LocalFilePath { get; set; }
    
    public string PreSignedUrl { get; set; }
}