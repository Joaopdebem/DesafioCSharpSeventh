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

    [HttpGet("servers/{IPAddress}:{IPPort}")]
    public async Task<IActionResult> GetServerByIP(string IPAddress, int IPPort)
    {
        var server = await _serverService.GetServerByIPAsync(IPAddress, IPPort);

        if (server == null)
        {
            return NotFound();
        }

        return Ok(server);
    }

    [HttpGet("servers/available/{id}")]
    public async Task<IActionResult> IsServerAvailable(Guid id)
    {
        bool isAvailable = await _serverService.IsServerAvailableAsync(id);

        if (isAvailable)
        {
            return Ok("Server is available.");
        }
        else
        {
            return NotFound("Server is not available.");
        }
    }

    [HttpPost("server")]
    public async Task<IActionResult> AddServer([FromBody] AddServerRequest request)
    {
        await _serverService.AddServerAsync(request);
        return Ok();
    }

    [HttpPut("servers/{id}")]
    public async Task<IActionResult> UpdateServer(Guid id, [FromBody] UpdateServerRequest request)
    {
        var existingServer = await _serverService.GetServerByIdAsync(id);

        if (existingServer == null)
        {
            return NotFound();
        }

        existingServer.Name = request.Name;
        existingServer.IPAddress = request.IPAddress;
        existingServer.IPPort = request.IPPort;

        await _serverService.UpdateServerAsync(id, request);

        return Ok();
    }

    [HttpDelete("servers/{id}")]
    public async Task<IActionResult> DeleteServer(Guid id)
    {
        var serverToDelete = await _serverService.GetServerByIdAsync(id);

        if (serverToDelete == null)
        {
            return NotFound();
        }

        await _serverService.DeleteServerAsync(id);

        return Ok();
    }

}
