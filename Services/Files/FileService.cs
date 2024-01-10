using DesafioCSharpSeventh.Models;

namespace DesafioCSharpSeventh.Services.Files;

public class FileService : IFileService
{
    private readonly string _path;

    public FileService(IConfiguration configuration)
    {
        _path = configuration.GetSection("Repository").GetSection("PathFile").Value;
        ValidatePath();
    }


    private void ValidatePath()
    {
        if (string.IsNullOrEmpty(_path))
        {
            throw new InvalidOperationException("O caminho do repositório não foi configurado corretamente.");
        }
    }


    public void RemoveFile(string fileName)
    {
        var fullPath = Path.GetFullPath(_path);
        File.Delete(fullPath + fileName);
    }


    public async Task<FileOut> SaveFileAsync(string base64)
    {
        try
        {
            var fullPath = Path.GetFullPath(_path);
            var file = Convert.FromBase64String(base64);
            var idFile = Guid.NewGuid();
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            using (FileStream stream = File.Open($@"{fullPath}{idFile}.bin", FileMode.OpenOrCreate))
            {
                await stream.WriteAsync(file, 0, file.Length);
            }

            var fileInfo = new FileInfo(fullPath + idFile + ".bin");
            long length = fileInfo.Length;
            return new FileOut(idFile, length);
        }
        catch (Exception ex)
        {
            return new FileOut(ex);
        }
    }


    public async Task<string> GetFileInBase64Async(string fileName)
    {
        ValidatePath();

        var bytes = await GetBinaryAsync(fileName);
        return Convert.ToBase64String(bytes);
    }


    public async Task<byte[]> GetBinaryAsync(string fileName)
    {
        ValidatePath();

        var fullPath = Path.Combine(_path, fileName);

        byte[] result;
        using (FileStream stream = File.Open(fullPath, FileMode.Open))
        {
            result = new byte[stream.Length];
            await stream.ReadAsync(result.AsMemory(0, result.Length));
        }

        return result;
    }


    private static bool IsBase64String(string s)
    {
        try
        {
            Convert.FromBase64String(s);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

}
