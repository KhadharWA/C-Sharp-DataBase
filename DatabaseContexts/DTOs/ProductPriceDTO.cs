

namespace Shared.DTOs;

public class ProductPriceDTO
{
    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public string CurrencyCode { get; set; } = null!;
}
