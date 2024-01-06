using DesafioCSharpSeventh.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioCSharpSeventh.Data;

public class AppDbContext : DbContext
{
    public DbSet<Server> Servers { get; set; }
    public DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString:"Data Source=Bd.ServerManager");
        base.OnConfiguring(optionsBuilder);
    }

}
