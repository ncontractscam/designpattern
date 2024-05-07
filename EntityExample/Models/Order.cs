namespace EntityExample.Models;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Total { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    public int OrderId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}