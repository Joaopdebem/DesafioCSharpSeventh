using DesafioCSharpSeventh.Models.BaseModels;

namespace DesafioCSharpSeventh.Models;

public class Video : Base
{
    public new Guid Id { get; set; }
    public Guid MediaFileId { get; set; }
    public string Description { get; set; }
    public long SizeInBytes { get; private set; }
    public Guid ServerId { get; set; }
    public Server Server { get; set; }

    public Video(string description, Guid mediaFileId)
    {
        Description = description;
        MediaFileId = mediaFileId;
        CreateDate = DateTime.UtcNow;
    }
}
