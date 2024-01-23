

namespace Shared.Models;

public class Product
{
    public string ArticleNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Ingress { get; set; } 

    public string? Description { get; set; }

    public string? Specification { get; set; }
    
}
