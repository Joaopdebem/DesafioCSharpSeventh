using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;

namespace DesafioCSharpSeventh.Services
{
    public interface IServerService
    {
        Task<IEnumerable<Server>> GetServersAsync();
        Task<Server> GetServerByIdAsync(Guid id);
        Task<Server> GetServerByIPAsync(string IPAddress, int IPPort);
        Task AddServerAsync(AddServerRequest request);
        Task UpdateServerAsync(Guid id, UpdateServerRequest request);
        Task DeleteServerAsync(Guid id);
        Task<bool> IsServerAvailableAsync(Guid id);
        
    }
}
