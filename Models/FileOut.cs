namespace DesafioCSharpSeventh.Models;

public class FileOut
{
    public Guid Id { get; }
    public long SizeInBytes { get; }
    public Exception Error { get; }

    public FileOut(Guid id, long sizeInBytes)
    {
        Id = id;
        SizeInBytes = sizeInBytes;
    }
    public FileOut(Exception exception)
    {
        Error = exception;
    }
}
