using System.Text;
using FileReceiverAndSender.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FileReceiverAndSender.Services;

public class FileService : IFileService
{
    public Task<string> Load(string input)
    {
        throw new NotImplementedException();
    }

    public Task<string> Save(Stream input, string fileName)
    {
        var localPath = @"C:\Users\sholomserebryanski\Repos\CSharpPlaygrounds\FileReceiverAndSender\Files";
        StreamContent
    }
}