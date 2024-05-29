namespace ScaffoldingImport.Models;

public partial class Item
{
    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public Guid uniqueItemId { get; set; }
}
