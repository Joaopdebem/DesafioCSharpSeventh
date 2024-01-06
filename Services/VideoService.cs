using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DesafioCSharpSeventh.Services;

public class VideoService : IVideoService
{
    private readonly AppDbContext _context;

    public VideoService(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddVideoAsync(Guid serverId, AddVideoRequest request)
    {
        var server = await _context.Servers.FindAsync(serverId);

        if (server != null)
        {
            var newVideo = new Video(request.Description, request.BinaryContent, request.ServerId);
            server.Videos.Add(newVideo);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Video>> GetVideosAsync(Guid serverId)
    {
        var server = await _context.Servers.Include(s => s.Videos).FirstOrDefaultAsync(s => s.Id == serverId);

        return server?.Videos ?? Enumerable.Empty<Video>();
    }

    public async Task<Video> GetVideoByIdAsync(Guid videoId)
    {
        return await _context.Videos.FindAsync(videoId);
    }

    public async Task DeleteVideoAsync(Guid videoId)
    {
        var videoToDelete = await _context.Videos.FindAsync(videoId);

        if (videoToDelete != null)
        {
            _context.Videos.Remove(videoToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public Task AddVideoAsync(Guid serverId, Video video)
    {
        throw new NotImplementedException();
    }

    public Task<Video> GetVideoByIdAsync(Guid serverId, Guid videoId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteVideoAsync(Guid serverId, Guid videoId)
    {
        throw new NotImplementedException();
    }
}
