namespace DesafioCSharpSeventh.Utilities
{
    public record AddVideoRequest(string Description, byte[] BinaryContent, Guid ServerId);
}
