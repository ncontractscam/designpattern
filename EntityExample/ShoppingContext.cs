using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst
{
    public class ShoppingContext : DbContext
    {
        public string DbPath { get; }

        public ShoppingContext()
        {
            DbPath = """C:\dev\shopping.db""";
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
