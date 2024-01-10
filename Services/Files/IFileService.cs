using DesafioCSharpSeventh.Models;

namespace DesafioCSharpSeventh.Services.Files;

public interface IFileService
{
    void RemoveFile(string fileName);
    Task<FileOut> SaveFileAsync(string base64);
    Task<string> GetFileInBase64Async(string fileName);
    Task<byte[]> GetBinaryAsync(string fileName);
}
