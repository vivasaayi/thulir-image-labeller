namespace ImageLabeller.WebModels;

public class Label
{
    public string LabelName { get; set; }
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }
}

public class ImageLabels
{
    public Image Image { get; set; }
    
    public List<Label> Labels { get; set; }

    public ImageLabels()
    {
        Image = new Image();
        Labels = new List<Label>();
    }
}