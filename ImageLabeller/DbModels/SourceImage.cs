using System.Text.Json.Serialization;

namespace ImageLabeller.DbModels;

public class SourceImage
{
    [JsonPropertyName("imageid")] 
    public Guid ImageId { get; set; }

    [JsonPropertyName("s3path")] 
    public string S3Path { get; set; }

    [JsonPropertyName("indexedtime")] 
    public DateTime IndexedTime { get; set; }
    
    [JsonPropertyName("rownumber")] 
    public int RowNumber { get; set; }
}