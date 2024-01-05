using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DesafioCSharpSeventh.Services
{
    public class ServerService : IServerService
    {
        private readonly AppDbContext _context;

        public ServerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddServerAsync(AddServerRequest request)
        {
            var newServer = new Server(request.Name, request.IPAdress, request.IPPort);
            await _context.Servers.AddAsync(newServer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Server>> GetServersAsync()
        {
            return await _context.Servers.ToListAsync();
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
                existingServer.IPAdress = request.IPAdress;
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
        public async Task<bool> IsServerAvailableAsync(Guid id)
        {
            var server = await _context.Servers.FindAsync(id);

            if (server == null)
            {
                Console.WriteLine($"Server with ID {id} not found.");
                return false;
            }

            bool isServerReachable = await CheckServerReachability(server.IPAdress, server.IPPort);

            return isServerReachable;
        }

        private async Task<bool> CheckServerReachability(string ipAddress, int port)
        {
            try
            {
                using (var client = new System.Net.Sockets.TcpClient())
                {
                    await client.ConnectAsync(ipAddress, port);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
