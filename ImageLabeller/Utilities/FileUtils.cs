namespace ImageLabeller.Utilities;

public class FileUtils
{
    public static string[] GetFilesFromFolder(string folderName)
    {
        var fileNames = Directory.GetFiles(folderName);
        return fileNames;
    }
}