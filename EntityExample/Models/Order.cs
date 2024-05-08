namespace CodeFirst.Models;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Total { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual List<Item> Items { get; set; }
}