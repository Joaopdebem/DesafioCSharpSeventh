using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Services;
using DesafioCSharpSeventh.Utilities;
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

    [HttpGet("servers/{serverId}/videos/{videoId}")]
    public async Task<IActionResult> GetVideoById(Guid serverId, Guid videoId)
    {
        var video = await _videoService.GetVideoByIdAsync(serverId, videoId);

        if (video == null)
        {
            return NotFound();
        }

        return Ok(video);
    }

    [HttpPost("servers/{serverId}/videos")]
    public async Task<IActionResult> AddVideo(Guid serverId, [FromBody] AddVideoRequest request)
    {
        await _videoService.AddVideoAsync(serverId, request);
        return Ok();
    }

    [HttpDelete("servers/{serverId}/videos/{videoId}")]
    public async Task<IActionResult> DeleteVideo(Guid serverId, Guid videoId)
    {
        var videoToDelete = await _videoService.GetVideoByIdAsync(serverId, videoId);

        if (videoToDelete == null)
        {
            return NotFound();
        }

        await _videoService.DeleteVideoAsync(serverId, videoId);

        return Ok();
    }
}
