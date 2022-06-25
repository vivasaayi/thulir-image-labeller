using System.Threading.Tasks;
using ImageLabeller.Repositories;
using NUnit.Framework;

namespace ImageLabellerUnitTest.ImageRepository;


public class ImagesRepositoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        ImagesRepository _imageRepo = new ImagesRepository();
        var imageNames = await _imageRepo.GetImageNames();

        Assert.GreaterOrEqual(imageNames.Count, 1000);
        Assert.AreEqual(imageNames[0].S3Path, "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1/IMG20220526180822.jpg");
        Assert.AreEqual(imageNames[0].S3Path, "IMG20220526180822.jpg");
        Assert.AreEqual(0, imageNames[0].ImageId);
        
        Assert.AreEqual(imageNames[3].S3Path, "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1/IMG20220527180544 1.jpg");
        Assert.AreEqual(imageNames[3].S3Path, "IMG20220527180544 1.jpg");
        Assert.AreEqual(imageNames[3].ImageId, 3);
        
        Assert.Pass();
    }
}