namespace DesafioCSharpSeventh.Models;

public class Video
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public byte[] BinaryContent  { get; set; }
    public Guid ServerId { get; set; }

    public Video()
    {
        
    }
    public Video(string description, byte[] binaryContent, Guid serverId)
    {
        Description = description;
        BinaryContent =binaryContent;
        ServerId = serverId;
    }

}
