using ApiPlayground.Models;

namespace ApiPlayground.services;

public interface IFileService
{
    Task<FileResultResponse> ReadFile();
    Task WriteFile(AddToFileRequest lines);
}