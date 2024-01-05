namespace DesafioCSharpSeventh.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte[] BinaryContent  { get; set; }
    }
}
