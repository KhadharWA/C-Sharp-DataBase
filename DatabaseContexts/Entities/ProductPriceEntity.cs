

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace Shared.Entities;

public class ProductPriceEntity
{
    [Key]
    [ForeignKey("ProductEntity")]
    public string ArticleNumber { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountPrice { get; set; }

    [Required]
    [StringLength(3)]
    public string CurrencyCode { get; set; } = null!;

    public virtual ProductEntity ProductEntity { get; set; } = null!;

    public virtual CurrencyEntity CurrencyEntity { get; set; } = null!;



}
