using ApiPlayground.controllers;
namespace ApiPlayground.services.interfaces;

public interface IFileService
{
    Task<FileResultResponse> ReadFile();
    Task WriteFile(AddToFileRequest lines);
}