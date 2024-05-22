using System;
using System.Collections.Generic;

namespace ScaffoldingImport.Models;

public partial class OrderItem
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public long ItemId { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
