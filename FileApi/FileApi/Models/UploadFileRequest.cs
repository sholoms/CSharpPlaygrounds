namespace FileApi.Models;

public class UploadFileRequest
{
    public FileStream File { get; set; }
    public string FileName { get; set; }
}