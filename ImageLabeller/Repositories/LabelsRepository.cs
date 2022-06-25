using ImageLabeller.DbModels;
using ImageLabeller.WebModels;

namespace ImageLabeller.Repositories;

public class LabelsRepository
{
    private ImageLabels _imageLabel = null;

    public LabelsRepository()
    {
        _imageLabel = new ImageLabels()
        {
            Image = new SourceImage()
            {
                ImageIndex = 123,
                S3Path = "cotton.jpg"
            }
        };
        
        _imageLabel.Labels.Add(new Label(){
            LabelName = "flower",
            X1 = 10,
            Y1 = 10,
            X2 = 100,
            Y2 = 100
        });
    }
    
    public ImageLabels GetLables(string imageId)
    {
        return _imageLabel;
    }
    public void SaveLabels(ImageLabels labels)
    {
        _imageLabel = labels;
    }
}