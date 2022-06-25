using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ImageLabeller.DbModels;

public class Point
{
    [JsonPropertyName("x")]
    public double X { get; set; }
    
    [JsonPropertyName("y")]
    public double Y { get; set; }
}

public class Label
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("label")]
    public string LabelName { get; set; }
    
    [JsonPropertyName("points")]
    public IList<Point> Points { get; set; }

    public Label()
    {
        Points = new List<Point>();
    }
}

public class ImageLabel
{
    public Guid ImageId { get; set; }
    public List<Label> Labels { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    public ImageLabel()
    {
        Labels = new List<Label>();
    }
}