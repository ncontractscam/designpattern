namespace CodeFirst.Models;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Total { get; set; }

    public int CustomerId { get; set; }

    public virtual List<OrderItem> OrderItem { get; set; }
}