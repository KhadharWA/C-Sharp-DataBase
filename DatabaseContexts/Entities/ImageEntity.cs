
using System.ComponentModel.DataAnnotations;

namespace Shared.Entities;

public class ImageEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Url]
    
    public string ImageUrl { get; set; } = null!;
}
