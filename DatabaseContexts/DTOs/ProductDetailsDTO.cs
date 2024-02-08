

namespace Shared.DTOs;

public class ProductDetailsDTO
{
    public string ArticleNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ManufacturerName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string? Ingress { get; set; }

    public string? Description { get; set; }

    public string? Specification { get; set; }

    public decimal PriceDetails { get; set; } 

    public string ImageUrl { get; set; } = null!;
}
