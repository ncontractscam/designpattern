namespace ScaffoldingImport.Models;

public partial class Order
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Total { get; set; }

    public long CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
