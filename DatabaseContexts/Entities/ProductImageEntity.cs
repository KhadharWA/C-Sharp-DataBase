


using System.ComponentModel.DataAnnotations.Schema;


namespace Shared.Entities;

public class ProductCurrencyEntity
{
    
    [Column(Order = 1)]
    public string ArticleNumber { get; set; } = null!;

    
    [Column(Order = 2)]
    public int ImageId { get; set; }

    [ForeignKey("ArticleNumber")]
    public virtual ProductEntity Product { get; set; } = null!;

    [ForeignKey("ImageId")]
    public virtual ImageEntity Image { get; set; } = null!;
}
