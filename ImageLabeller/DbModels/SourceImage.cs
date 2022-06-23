using System.Text.Json.Serialization;

namespace ImageLabeller.DbModels;

public class SourceImage
{
    [JsonPropertyName("dt")] 
    public double TimeStamp { get; set; }

    [JsonPropertyName("temp_min")] 
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")] 
    public double TempMax { get; set; }

    [JsonPropertyName("pressure")] 
    public double Pressure { get; set; }
}