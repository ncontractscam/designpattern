namespace CodeFirst.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    public virtual Item Item { get; set; }
}