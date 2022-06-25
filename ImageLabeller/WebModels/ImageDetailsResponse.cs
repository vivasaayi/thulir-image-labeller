using ImageLabeller.DbModels;

namespace ImageLabeller.WebModels;

public class ImageDetailsResponse
{
    public SourceImage SourceImage { get; set; }
    public ImageLabel ImageLabels { get; set; }
}