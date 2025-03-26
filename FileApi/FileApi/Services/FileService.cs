using FileApi.Models;
using FileApi.Services.interfaces;
using Microsoft.Extensions.Options;

namespace FileApi.Services;

public class FileService : IFileService
{
    public FileStream DownloadFile(string fileName)
    {
        var basePath = @"C:\Users\sholomserebryanski\Repos\CSharpPlaygrounds\FileApi\Files\";
        var file = $"{basePath}{fileName}.pdf";
        var sourceStream = File.Open(file, FileMode.Open);
        return sourceStream;
    }

    public Task UploadFile(UploadFileRequest lines)
    {
        throw new NotImplementedException();
       
    }
}