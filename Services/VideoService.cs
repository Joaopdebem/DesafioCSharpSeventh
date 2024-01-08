using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Models.Projections;
using DesafioCSharpSeventh.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System;

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
        var server = await _context.Servers
            .FirstOrDefaultAsync(s => s.Id == serverId);

        if (server != null)
        {
            var newVideo = new Video(request.Description);
            server.Videos.Add(newVideo);
            await _context.SaveChangesAsync();
        }
    }



    public async Task<IEnumerable<VideoProjection>> GetVideosAsync(Guid serverId)
    {
        var server = await _context.Servers.Include(s => s.Videos).FirstOrDefaultAsync(s => s.Id == serverId);

        return server?.Videos.Select(video => new VideoProjection
        {
            Id = video.Id,
            Description = video.Description,
        }) ?? Enumerable.Empty<VideoProjection>();
    }


    public async Task<VideoProjection?> GetVideoByIdAsync(Guid serverId, Guid videoId)
    {
        var video = await _context.Videos.FindAsync(videoId);

        return video != null ? new VideoProjection
        {
            Id = video.Id,
            Description = video.Description,
        } : null;
    }


    public async Task UpdateVideoAsync(Guid videoId, UpdateVideoRequest request)
    {
        var existingVideo = await _context.Videos.FindAsync(videoId);

        if (existingVideo != null)
        {
            existingVideo.Description = request.Description;
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteVideoAsync(Guid serverId, Guid videoId)
    {
        var videoToDelete = await _context.Videos.FindAsync(videoId);

        if (videoToDelete != null)
        {
            _context.Videos.Remove(videoToDelete);
            await _context.SaveChangesAsync();
        }
    }

}
