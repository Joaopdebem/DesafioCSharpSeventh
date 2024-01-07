using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;

namespace DesafioCSharpSeventh.Services;

public class ServerService : IServerService
{
    private readonly AppDbContext _context;

    
    public ServerService(AppDbContext context)
    {
        _context = context;
    }
    
    
    public async Task AddServerAsync(AddServerRequest request)
    {
        var newServer = new Server(request.Name, request.IPAddress, request.IPPort);
        await _context.Servers.AddAsync(newServer);
        await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<Server>> GetServersAsync()
    {
        return await _context.Servers.Include(s => s.Videos).ToListAsync();
    }

    
    public async Task<Server> GetServerByIdAsync(Guid id)
    {
        return await _context.Servers.FindAsync(id);
    }
    
    
    public async Task UpdateServerAsync(Guid id, UpdateServerRequest request)
    {
        var existingServer = await _context.Servers.FindAsync(id);

        if (existingServer != null)
        {
            existingServer.Name = request.Name;
            existingServer.IPAddress = request.IPAddress;
            existingServer.IPPort = request.IPPort;

            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteServerAsync(Guid id)
    {
        var serverToDelete = await _context.Servers.FindAsync(id);

        if (serverToDelete != null)
        {
            _context.Servers.Remove(serverToDelete);
            await _context.SaveChangesAsync();
        }
    }
    
    
    public async Task<bool> AvailableAsync(string serverId)
    {
        var server = await _context.Servers.FindAsync(serverId);
        return await PingHostAsync(server.IPAddress, server.IPPort);
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
