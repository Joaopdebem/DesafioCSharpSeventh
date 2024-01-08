using DesafioCSharpSeventh.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace DesafioCSharpSeventh.Models;

public class Server : Base
{
    public const string requiredMessage = "Campo obrigatório";
    public const string ipMaskMessage = "Formato de IP inválido";


    [Required(ErrorMessage = requiredMessage)]
    public string Name { get; set; }


    [Required(ErrorMessage = requiredMessage)]
    [RegularExpression(@"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b", ErrorMessage = ipMaskMessage)]
    public string Ip { get; set; }


    [Required(ErrorMessage = requiredMessage)]
    public int Port { get; set; }


    public List<Video> Videos { get; set; } = new List<Video>();

    public Server(string name, string ip, int port)
    {
        Name = name;
        Ip = ip;
        Port = port;
        CreateDate = DateTimeOffset.UtcNow;
        IsActive = true;
        IsDeleted = false;
    }
}
