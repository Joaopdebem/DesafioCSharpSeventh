using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Models.Projections;
using DesafioCSharpSeventh.Utilities;

namespace DesafioCSharpSeventh.Services;

public interface IVideoService
{
    Task<IEnumerable<VideoProjection>> GetVideosAsync(Guid serverId);
    Task AddVideoAsync(Guid serverId, AddVideoRequest request);
    Task<VideoProjection> GetVideoByIdAsync(Guid serverId, Guid videoId);
    Task UpdateVideoAsync(Guid videoId, UpdateVideoRequest request);
    Task DeleteVideoAsync(Guid serverId, Guid videoId);
}
