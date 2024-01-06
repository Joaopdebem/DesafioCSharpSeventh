using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCSharpSeventh.Controllers;

[ApiController]
[Route("api")]
public class VideoController : Controller
{
    private readonly IVideoService _videoService;

    public VideoController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpGet("servers/{serverId}/videos")]
    public async Task<IActionResult> GetVideos(Guid serverId)
    {
        var videos = await _videoService.GetVideosAsync(serverId);
        return Ok(videos);
    }

    [HttpPost("servers/{serverId}/videos")]
    public async Task<IActionResult> AddVideo(Guid serverId, [FromBody] Video video)
    {
        await _videoService.AddVideoAsync(serverId, video);
        return Ok();
    }

    [HttpGet("videos/{videoId}")]
    public async Task<IActionResult> GetVideoById(Guid videoId)
    {
        var video = await _videoService.GetVideoByIdAsync(videoId);

        if (video == null)
        {
            return NotFound();
        }

        return Ok(video);
    }

    [HttpDelete("videos/{videoId}")]
    public async Task<IActionResult> DeleteVideo(Guid videoId)
    {
        var videoToDelete = await _videoService.GetVideoByIdAsync(videoId);

        if (videoToDelete == null)
        {
            return NotFound();
        }

        await _videoService.DeleteVideoAsync(videoId);

        return Ok();
    }
}
