using System.Text.Json.Serialization;

namespace ImageLabeller.DbModels;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Label
{
    public int Id { get; set; }
    public string LabelName { get; set; }
    
    public IList<Point> Points { get; set; }

    public Label()
    {
        Points = new List<Point>();
    }
}

public class ImageLabel
{
    public Guid ImageId { get; set; }
    public Label Labels { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

}