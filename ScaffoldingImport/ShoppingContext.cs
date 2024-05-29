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
            entity.HasKey(i => i.uniqueItemId);
            entity.ToTable("Item");
            entity.Property(o => o.uniqueItemId).HasColumnName("Guid");
        });
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");
            entity.Property(oi => oi.OrderId).HasColumnName("OrderId");
            entity.HasOne(oi => oi.Item).WithMany().HasForeignKey(oi => oi.ItemGuid);
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(o => o.OrderId);
            entity.Property(o => o.OrderId).HasColumnName("Id");
            entity.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(o => o.OrderId);
        });
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.Property(c => c.Address).HasMaxLength(50);
        });
    }
}
