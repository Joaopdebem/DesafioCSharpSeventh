using DesafioCSharpSeventh.Models;
using DesafioCSharpSeventh.Utilities;

namespace DesafioCSharpSeventh.Services
{
    public interface IServerService
    {
        Task<IEnumerable<Server>> GetServersAsync();
        Task<Server> GetServerByIdAsync(Guid id);
        Task AddServerAsync(AddServerRequest request);
        //Task UpdateServerAsync(Guid id, UpdateServerRequest request);
        //Task DeleteServerAsync(Guid id);
    }
}
