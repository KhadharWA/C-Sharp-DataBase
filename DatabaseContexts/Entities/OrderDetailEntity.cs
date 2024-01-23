



using Shared.Entities;

namespace Shared.Models;

public class OrderDetailEntity 
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public OrderEntity Order { get; set; } = null!;

    public int ProductId { get; set; }

    public ProductEntity Product { get; set; } = null!;

    public int Quantity { get; set; }
}
