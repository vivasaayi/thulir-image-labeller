namespace ImageLabeller.Models;

public class S3SyncResult{
    public string S3Key { get; set; }
    public int NumberOfRecords { get; set; }
    public int Success { get; set; }
    public int Failed { get; set; }
}