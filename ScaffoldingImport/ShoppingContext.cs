using Microsoft.EntityFrameworkCore;
using ScaffoldingImport.Models;

namespace ScaffoldingImport;

public partial class ShoppingContext : DbContext
{
    public ShoppingContext()
    {
    }

    public ShoppingContext(DbContextOptions<ShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EF;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(i => i.Guid);
            entity.ToTable("Item");
        });
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");
            entity.Property(oi => oi.ZOrderId).HasColumnName("OrderId");
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(o => o.ZOrderId);
            entity.Property(o => o.ZOrderId).HasColumnName("Id");
            entity.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(o => o.ZOrderId);
        });
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.Property(c => c.Address).HasMaxLength(50);
        });
    }
}
