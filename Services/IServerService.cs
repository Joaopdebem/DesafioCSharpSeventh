using DesafioCSharpSeventh.Models.Projections;
using DesafioCSharpSeventh.Utilities;

namespace DesafioCSharpSeventh.Services;

public interface IServerService
{
    Task<IEnumerable<ServerProjection>> GetServersAsync();
    Task<ServerProjection> GetServerByIdAsync(Guid id);
    Task<Guid> AddServerAsync(AddServerRequest request);
    Task UpdateServerAsync(Guid id, UpdateServerRequest request);
    Task<bool> DeleteServerAsync(Guid id);
    Task<bool> AvailableAsync(string serverId);
    
}
