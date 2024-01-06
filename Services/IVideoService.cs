using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;

namespace DesafioCSharpSeventh.Services;

public interface IVideoService
{
    Task<IEnumerable<Video>> GetVideosAsync(Guid serverId);
    Task AddVideoAsync(Guid serverId, AddVideoRequest request);
    Task<Video> GetVideoByIdAsync(Guid serverId, Guid videoId);
    Task DeleteVideoAsync(Guid serverId, Guid videoId);
}
