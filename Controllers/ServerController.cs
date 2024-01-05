using DesafioCSharpSeventh.Services;
using DesafioCSharpSeventh.Utilities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ServerController : ControllerBase
{
    private readonly IServerService _serverService;

    public ServerController(IServerService serverService)
    {
        _serverService = serverService;
    }

    [HttpGet("servers")]
    public async Task<IActionResult> GetServers()
    {
        var servers = await _serverService.GetServersAsync();
        return Ok(servers);
    }

    [HttpGet("servers/{id}")]
    public async Task<IActionResult> GetServerById(Guid id)
    {
        var server = await _serverService.GetServerByIdAsync(id);

        if (server == null)
        {
            return NotFound();
        }

        return Ok(server);
    }

    [HttpPost("server")]
    public async Task<IActionResult> AddServer([FromBody] AddServerRequest request)
    {
        await _serverService.AddServerAsync(request);
        return Ok();
    }

}
