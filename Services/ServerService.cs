using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server;
using DesafioCSharpSeventh.Models.Projections;

namespace DesafioCSharpSeventh.Services;

public class ServerService : IServerService
{
    private readonly AppDbContext _context;

    
    public ServerService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Guid> AddServerAsync(AddServerRequest request)
    {
        var newServer = new Server(request.Name, request.Ip, request.Port);
        _context.Servers.Add(newServer);
        await _context.SaveChangesAsync();

        return newServer.Id;
    }


    public async Task<IEnumerable<ServerProjection>> GetServersAsync()
    {
        var servers = await _context.Servers
            .Select(server => new ServerProjection
            {
                Id = server.Id,
                Name = server.Name,
                Ip = server.Ip,
                Port = server.Port,
            })
            .ToListAsync();

        return servers;
    }


    public async Task<ServerProjection> GetServerByIdAsync(Guid id)
    {
        var serverProjection = await _context.Servers
            .Where(s => s.Id == id)
            .Select(s => new ServerProjection
            {
                Id = s.Id,
                Name = s.Name,
                Ip = s.Ip,
                Port = s.Port,
            })
            .FirstOrDefaultAsync();

        return serverProjection;
    }


    public async Task UpdateServerAsync(Guid id, UpdateServerRequest request)
    {
        var existingServer = await _context.Servers.FindAsync(id);

        if (existingServer != null)
        {
            existingServer.Name = request.Name;
            existingServer.Ip = request.Ip;
            existingServer.Port = request.Port;

            await _context.SaveChangesAsync();
        }
    }


    public async Task<bool> DeleteServerAsync(Guid id)
    {
        var serverToDelete = await _context.Servers.FindAsync(id);

        if (serverToDelete != null)
        {
            _context.Servers.Remove(serverToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }


    public async Task<bool> AvailableAsync(string serverId)
    {
        if (Guid.TryParse(serverId, out var serverGuid))
        {
            var server = await _context.Servers.FindAsync(serverGuid);

            if (server != null)
            {
                return await PingHostAsync(server.Ip, server.Port);
            }
        }
        return false;
    }


    public async Task<bool> PingHostAsync(string ip, int port)
    {
        try
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            using (var client = new TcpClient())
            {
                await client.ConnectAsync(ipAddress, port);
                return client.Connected;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar no servidor: {ex.Message}");
            return false;
        }
    }
}
