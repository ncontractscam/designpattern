namespace ScaffoldingImport.Models;

public partial class OrderItem
{
    public long Id { get; set; }

    public long ZOrderId { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public Guid ItemGuid { get; set; }

    public virtual Order Order { get; set; } = null!;
}
