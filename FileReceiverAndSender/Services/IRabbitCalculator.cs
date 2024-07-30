namespace FileReceiverAndSender.Services;

public interface IFileService
{
    Task<string> Load(string input);
    Task<string> Save(Stream input);
}