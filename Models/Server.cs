namespace DesafioCSharpSeventh.Models;

public class Server
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string IPAddress { get; set; }
    public int IPPort { get; set; }
    public List<Video> Videos { get; set; } = new List<Video>();

    public Server()
    {
        Id = Guid.NewGuid();
    }

    public Server(string name, string ipaddress, int ipport)
    {
        Name = name;
        IPAddress = ipaddress;
        IPPort = ipport;
    }
}
