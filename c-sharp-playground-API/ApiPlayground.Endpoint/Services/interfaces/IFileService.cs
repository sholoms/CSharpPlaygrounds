using ApiPlayground.controllers;
namespace ApiPlayground.services.interfaces;

public interface IFileService
{
    Task<StorageResultResponse> ReadFile();
    Task WriteFile(AddToStorageRequest lines);
    Task WriteFile(string line);
}