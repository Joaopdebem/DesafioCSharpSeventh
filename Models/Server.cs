namespace DesafioCSharpSeventh.Models
{
    public class Server
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string IPAdress { get; set; }
        public int IPPort { get; set; }

        public Server()
        {
            Id = Guid.NewGuid();
        }

        public Server(string name, string ipadress, int ipport)
        {
            Name = name;
            IPAdress = ipadress;
            IPPort = ipport;
        }
    }
}
