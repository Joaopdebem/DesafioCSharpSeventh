using System.ComponentModel.DataAnnotations;

namespace DesafioCSharpSeventh.Models.BaseModels;
public abstract class Base
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;

}
