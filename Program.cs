using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Services;
using Microsoft.OpenApi.Models;

namespace DesafioCSharpSeventh;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Seventh C#", Version = "v1" });
        });
        builder.Services.AddScoped<AppDbContext>();
        builder.Services.AddScoped<IServerService, ServerService>();
        builder.Services.AddScoped<IVideoService, VideoService>();
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Seventh C# v1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.MapControllers();

        app.Run();
    }

}