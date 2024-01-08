using DesafioCSharpSeventh.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace DesafioCSharpSeventh.Models;

public class Video : Base
{
    public const string requiredMessage = "Campo obrigatório";

    public Guid Id { get; set; }

    [Required(ErrorMessage = requiredMessage)]
    [ConcurrencyCheck]
    public string Description { get; set; }


    [Required(ErrorMessage = requiredMessage)]
    public long SizeInBytes  { get; private set; }


    [Required(ErrorMessage = requiredMessage)]
    public Guid ServerId { get; set; }

    public Video(string description)
    {
        Description = description;
        CreateDate = DateTime.UtcNow;
    }

}
