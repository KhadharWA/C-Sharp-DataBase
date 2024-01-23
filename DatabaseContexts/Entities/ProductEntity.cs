



using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Shared.Entities;

public class ProductEntity 
{
    [Key]
    [StringLength(225)]
    public string ArticleNumber { get; set; } = null!;

    [Required]
    [StringLength(200)] 
    public string Title { get; set; } = null!;

    [ForeignKey("ManufacturesEntity")]
    public int ManufacturerId { get; set; } 

    [ForeignKey("CategoryEntity")]
    public int CategoryId { get; set; } 

    [Required]
    public string Ingress { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Specification { get; set; } = null!;

    
    public virtual ManufacturesEntity ManufacturesEntity { get; set; } = null!;
    public virtual CategoryEntity CategoryEntity { get; set; } = null!;

}
