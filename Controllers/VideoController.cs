using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Services;
using DesafioCSharpSeventh.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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


    [HttpPost("servers/{serverId}/videos")]
    [SwaggerOperation(Summary = "Adicionar um vídeo a um servidor", Description = "Endpoint para adicionar um vídeo a um servidor")]
    public async Task<IActionResult> AddVideo(Guid serverId, [FromBody] AddVideoRequest request)
    {
        Console.WriteLine("Chegou ate aqui");
        await _videoService.AddVideoAsync(serverId, request);
        return StatusCode(201, new
        {
            Message = "Video adicionado com sucesso"
        });
    }


    [HttpDelete("servers/{serverId}/videos/{videoId}")]
    [SwaggerOperation(Summary = "Excluir um vídeo de um servidor", Description = "Endpoint para remover um video de um servidor")]
    public async Task<IActionResult> DeleteVideo(Guid serverId, Guid videoId)
    {
        var videoToDelete = await _videoService.GetVideoByIdAsync(serverId, videoId);

        if (videoToDelete == null)
        {
            return NotFound(new
            {
                Message = "Video não encontrado"
            });
        }

        await _videoService.DeleteVideoAsync(serverId, videoId);

        return NoContent();
    }


    [HttpGet("servers/{serverId}/videos/{videoId}")]
    [SwaggerOperation(Summary = "Recuperar dados de um vídeo", Description = "Endpoint para recuperar dados cadastrais de um video em um servidor")]
    public async Task<IActionResult> GetVideoById(Guid serverId, Guid videoId)
    {
        var video = await _videoService.GetVideoByIdAsync(serverId, videoId);

        if (video == null)
        {
            return NotFound(new
            {
                Message = "Video não encontrado"
            });
        }

        return Ok(video);
    }
    

    [HttpGet("servers/{serverId}/videos")]
    [SwaggerOperation(Summary = "Listar todos vídeos de um servidor", Description = "Endpoint para listar todos os videos de um servidor")]
    public async Task<IActionResult> GetVideos(Guid serverId)
    {
        var videos = await _videoService.GetVideosAsync(serverId);

        if (videos == null)
        {
            return NotFound(new
            {
                Message = "Videos não encontrados"
            });
        }

        return Ok(videos);
    }

    
    [HttpPatch("servers/{serverId}/videos/{videoId}")]
    [SwaggerOperation(Summary = "Atualizar descrição de um vídeo", Description = "Endpoint para atualizar a descrição de um vídeo")]
    public async Task<IActionResult> UpdateVideo(Guid serverId, Guid videoId, [FromBody] UpdateVideoRequest request)
    {
        var existingVideo = await _videoService.GetVideoByIdAsync(serverId, videoId);

        if (existingVideo == null)
        {
            return NotFound(new
            {
                Message = "Video não encontrado"
            });
        }

        existingVideo.Description = request.Description;
        await _videoService.UpdateVideoAsync(videoId, request);

        return Ok();
    }


}
