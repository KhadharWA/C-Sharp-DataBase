



using System.ComponentModel.DataAnnotations;

namespace Shared.Entities;

public class CurrencyEntity 
{
    [Key]
    [StringLength(3)] 
    public string Code { get; set; } = null!;

    [Required]
    [StringLength(20)] 
    public string CurrencyName { get; set; } = null!;
}
