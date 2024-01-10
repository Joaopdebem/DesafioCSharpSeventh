using DesafioCSharpSeventh.Models;
using Microsoft.EntityFrameworkCore;
using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Models.Projections;
using DesafioCSharpSeventh.Services.Files;
using DesafioCSharpSeventh.Utilities;

namespace DesafioCSharpSeventh.Services;

public class VideoService : IVideoService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;
    private bool _isRunning = false;

    public VideoService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }


    public async Task AddVideoAsync(Guid serverId, AddVideoRequest request)
    {
        var server = await _context.Servers
            .FirstOrDefaultAsync(s => s.Id == serverId);

        if (server != null)
        {
            var mediaFile = await _fileService.SaveFileAsync(request.VideoBase64);
            var newVideo = new Video(request.Description, mediaFile.Id);

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


    public async Task<string> GetVideoBinaryBase64Async(Guid serverId, Guid videoId)
    {
        var video = await _context.Videos.FindAsync(videoId);

        if (video != null)
        {
            var fileName = $"{video.MediaFileId}.bin";
            var bytes = await _fileService.GetBinaryAsync(fileName);

            if (bytes != null && bytes.Length > 0)
            {
                return Convert.ToBase64String(bytes);
            }
        }

        return null;
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

            var fileName = $"{videoToDelete.MediaFileId}.bin";
            _fileService.RemoveFile(fileName);

            await _context.SaveChangesAsync();
        }
    }


    public async Task StartRecyclingAsync(int days)
    {
        if (_isRunning)
        {
            throw new InvalidOperationException("O processo de reciclagem já está em execução.");
        }

        await Task.Run(async () =>
        {
            _isRunning = true;

            try
            {
                var referenceDate = DateTime.UtcNow.AddDays(-days);

                var videosToRecycle = _context.Videos
                    .Include(v => v.Server)
                    .AsEnumerable()
                    .Where(v => v.CreateDate <= referenceDate)
                    .ToList();

                foreach (var video in videosToRecycle)
                {
                    _context.Videos.Remove(video);

                    var fileName = $"{video.MediaFileId}.bin";
                    _fileService.RemoveFile(fileName);
                }

                await _context.SaveChangesAsync();
            }
            finally
            {
                _isRunning = false;
            }
        });
    }


    public Task<string> GetRecyclingStatusAsync()
    {
        return Task.FromResult(_isRunning ? "running" : "not running");
    }


}
