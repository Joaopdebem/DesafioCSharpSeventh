namespace DesafioCSharpSeventh.Models;

public class Video
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public long BinaryContent  { get; set; }
    public Guid ServerId { get; set; }

    public Video()
    {
        Id = Guid.NewGuid();
    }
    public Video(string description, long binaryContent, Guid serverId)
    {
        Description = description;
        BinaryContent = binaryContent;
        ServerId = serverId;
    }

}
