using ImageLabeller.Dals;
using ImageLabeller.Models;
using ImageLabeller.Repositories;
using ImageLabeller.Utilities;
using NUnit.Framework;

namespace ImageLabellerUnitTest.Utillities;

public class FileUtilsTests
{
    private string _path =
        "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetFilesFromFolder_Should_List_Files_With_Full_Path()
    {
        string[] files = FileUtils.GetFilesFromFolder(_path);
        Assert.AreEqual(files[0], "/Users/rajanp/Library/CloudStorage/OneDrive-SharedLibraries-onedrive/datasets/cotton/cotton sample 1/IMG20220526180822.jpg");
        Assert.GreaterOrEqual(files.Length, 100);
    }
}