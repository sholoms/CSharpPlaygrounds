using FileApi.Models;

namespace FileApi.Services.interfaces;

public interface IFileService
{
    FileStream DownloadFile(string fileName);
    Task UploadFile(UploadFileRequest lines);
}