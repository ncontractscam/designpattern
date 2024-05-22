﻿using System;
using System.Collections.Generic;

namespace ScaffoldingImport.Models;

public partial class Item
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
