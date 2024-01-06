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
    public Video(string description, string binaryContent, Guid serverId)
    {
        Description = description;
        BinaryContent = Convert.FromBase64String(binaryContent);
        ServerId = serverId;
    }

}
