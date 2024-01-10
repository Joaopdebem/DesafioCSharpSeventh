using DesafioCSharpSeventh.Services;
using DesafioCSharpSeventh.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
/*teste
*/

namespace DesafioCSharpSeventh.Controllers;

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
    [SwaggerOperation(Summary = "Listar todos servidores", Description = "Endpoint para listar todos servidores.")]
    public async Task<IActionResult> GetServers()
    {
        var servers = await _serverService.GetServersAsync();
        var serverProjections = servers.Select(s => new
        {
            s.Id,
            s.Name,
            s.Ip,
            s.Port
        }).ToList();

        return Ok(serverProjections);
    }


    [HttpGet("servers/{id}")]
    [SwaggerOperation(Summary = "Recuperar servidor", Description = "Endpoint para recuperar servidor por Id.")]
    public async Task<IActionResult> GetServerById(Guid id)
    {
        var server = await _serverService.GetServerByIdAsync(id);

        if (server == null)
        {
            return NotFound();
        }

        var serverProjection = new
        {
            server.Id,
            server.Name,
            server.Ip,
            server.Port
        };

        return Ok(serverProjection);
    }


    [HttpPost("server")]
    [SwaggerOperation(Summary = "Criar um novo servidor", Description = "Endpoint para criar um novo servidor.")]
    public async Task<IActionResult> AddServer([FromBody] AddServerRequest request)
    {
        var serverId = await _serverService.AddServerAsync(request);

        return StatusCode(201, new
        {
            Message = "Servidor criado com sucesso"
        });
    }


    [HttpPatch("servers/{id}")]
    [SwaggerOperation(Summary = "Atualizar um servidor", Description = "Endpoint para modificar um servidor")]
    public async Task<IActionResult> UpdateServer(Guid id, [FromBody] UpdateServerRequest request)
    {
        var existingServer = await _serverService.GetServerByIdAsync(id);

        if (existingServer == null)
        {
            return NotFound();
        }

        existingServer.Name = request.Name;
        existingServer.Ip = request.Ip;
        existingServer.Port = request.Port;

        await _serverService.UpdateServerAsync(id, request);

        return Ok();
    }


    [HttpDelete("servers/{id}")]
    [SwaggerOperation(Summary = "Excluir servidor", Description = "Endpoint para excluir um servidor")]
    public async Task<IActionResult> DeleteServer(Guid id)
    {
        var serverToDelete = await _serverService.GetServerByIdAsync(id);

        if (serverToDelete == null)
        {
            return NotFound(new
            {
                Message = "Servidor não encontrado"
            });
        }

        await _serverService.DeleteServerAsync(id);

        return NoContent();
        ;
    }


    [HttpGet("servers/available/{id}")]
    [SwaggerOperation(Summary = "Verificar disponibilidade de um servidor", Description = "Endpoint para checar disponibilidade.")]
    public async Task<IActionResult> IsServerAvailable(Guid id)
    {
        bool isAvailable = await _serverService.AvailableAsync(id.ToString());

        if (isAvailable)
        {
            return Ok("Servidor disponível");
        }
        else
        {
            return NotFound("Servidor indisponível");
        }
    }

}
