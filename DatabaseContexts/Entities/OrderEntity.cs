



namespace Shared.Models;

public class OrderEntity 
{
    public int OrderId { get; set; }

    public List<OrderDetailEntity> OrderDetails { get; set; } = null!;

    public DateTime OrderDate { get; set; }
}
