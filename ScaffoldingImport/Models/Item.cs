using System;
using System.Collections.Generic;

namespace ScaffoldingImport;

public partial class Item
{
    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public Guid Guid { get; set; }
}
