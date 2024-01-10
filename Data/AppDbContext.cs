using DesafioCSharpSeventh.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioCSharpSeventh.Data;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Server> Servers { get; set; }
    public DbSet<Video> Videos { get; set; }

    public static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
            .UseSqlite(connectionString: "Data Source=Bd.ServerManager");
        base.OnConfiguring(optionsBuilder);
    }

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
