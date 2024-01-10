using DesafioCSharpSeventh.Data;
using DesafioCSharpSeventh.Repository;
using DesafioCSharpSeventh.Services;
using DesafioCSharpSeventh.Services.Files;
using Microsoft.Extensions.Options;
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
            c.EnableAnnotations();
        });

        builder.Services.AddScoped<AppDbContext>();
        builder.Services.AddScoped<IServerService, ServerService>();
        builder.Services.AddScoped<IVideoService, VideoService>();
        builder.Services.AddScoped<IFileService, FileService>();

        builder.Services.AddControllers();


        var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();

        builder.Services.Configure<RepositorySettings>(configuration.GetSection("Repository"));
        builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<RepositorySettings>>().Value);


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