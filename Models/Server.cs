using DesafioCSharpSeventh.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace DesafioCSharpSeventh.Models;

public class Server : Base
{
    public string Name { get; set; }

    [RegularExpression(@"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b", ErrorMessage = "Formato de Ip inválido.")]
    public string Ip { get; set; }
    public int Port { get; set; }
    public List<Video> Videos { get; set; } = new List<Video>();

    public Server(string name, string ip, int port)
    {
        this.Name = name;
        this.Ip = ip;
        this.Port = port;
        this.CreateDate = DateTimeOffset.UtcNow;
    }
}
