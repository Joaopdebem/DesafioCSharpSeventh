using DesafioCSharpSeventh.Models;

namespace DesafioCSharpSeventh.Services;

public interface IVideoService
{
    Task<IEnumerable<Video>> GetVideosAsync(Guid serverId);
    Task AddVideoAsync(Guid serverId, Video video);
    Task<Video> GetVideoByIdAsync(Guid videoId);
    Task DeleteVideoAsync(Guid videoId);
}
