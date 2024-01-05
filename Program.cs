using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Services;
using DesafioCSharpSeventh.Utilities;
using Microsoft.Extensions.DependencyInjection;


namespace DesafioCSharpSeventh;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<AppDbContext>();
        builder.Services.AddScoped<IServerService, ServerService>();
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.MapControllers();

        app.Run();
    }

}