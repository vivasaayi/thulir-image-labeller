namespace ImageLabeller.WebModels;

public class Image
{
    public int ImageIndex { get; set; }
    public Guid ImageId { get; set; }
    
    public string ImageName { get; set; }
    public string ImageLocation { get; set; }
    
    public string PreSignedUrl { get; set; }
}