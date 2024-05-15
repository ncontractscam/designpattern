using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--Items--");
            await GetAllItems();
            await AddNewItem();
            await GetAllItems();

            Console.WriteLine("--Customers--");
            await GetAllCustomers();
            await AddNewCustomer();
            await GetAllCustomers();

            Console.WriteLine("--Orders--");
            await AddNewOrder();
        }

        public static async Task GetAllItems()
        {
            List<Item> results;
            await using (var db = new ShoppingContext())
            {
                results = db.Items.ToList();
            }

            var formatedResults = results.Select(x => $"Name: {x.Name}, Price: {x.UnitPrice}").ToList();
            Console.WriteLine($"Current DB results: {string.Join('|', formatedResults)}");
        }

        public static async Task AddNewItem()
        {

            Console.WriteLine("Enter a name for a new item: ");
            decimal cost = 0;
            var name = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter the price: ");
            } while (!decimal.TryParse(Console.ReadLine(), out cost));

            Console.WriteLine($"Adding {name} at cost ${cost}");
            var item = new Item { Name = name, UnitPrice = cost };


            await using (var db = new ShoppingContext())
            {
                db.Items.Add(item);
                await db.SaveChangesAsync();
            }
        }

        public static async Task GetAllCustomers()
        {
            List<Customer> results;
            await using (var db = new ShoppingContext())
            {
                results = db.Customers.ToList();
            }

            var formatedResults = results.Select(x => $"Name: {x.Name}, Address: {x.Address}").ToList();
            Console.WriteLine($"Current DB results: {string.Join('|', formatedResults)}");
        }

        public static async Task AddNewCustomer()
        {

            Console.WriteLine("Enter a name for a new customer: ");

            var name = Console.ReadLine();

            Console.WriteLine("Enter the address: ");
            var address = Console.ReadLine();

            Console.WriteLine($"Adding {name} with address {address}");
            var customer = new Customer { Name = name, Address = address };


            await using (var db = new ShoppingContext())
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
            }
        }

        public static async Task<Customer> GetCustomer()
        {
            Customer customer = null;

            while (customer == null)
            {
                Console.WriteLine("Enter a name for an existing customer: ");

                var custName = Console.ReadLine();

                await using (var db = new ShoppingContext())
                {
                    customer = await db.Customers.FirstOrDefaultAsync(c => c.Name == custName);
                }
            }

            return customer;
        }

        public static async Task AddNewOrder()
        {
            Console.WriteLine("Enter a name for an existing customer: ");

            var custName = Console.ReadLine();

            Console.WriteLine("Enter a name for a new order: ");
            var name = Console.ReadLine();

            int id = 0;
            do
            {
                Console.WriteLine("Enter the item ID to add to order: ");
            } while (!int.TryParse(Console.ReadLine(), out id));


            await using (var db = new ShoppingContext())
            {
                var customer = await db.Customers.FirstOrDefaultAsync(c => c.Name == custName);
                if (customer == null)
                {
                    throw new Exception();
                }

                var item = await db.Items.FirstOrDefaultAsync(x => x.Id == id);

                // decimal total = 0;
                var order = new Order
                {
                    Name = name,
                    Total = item.UnitPrice,
                    OrderItem = new List<OrderItem> {
                    new OrderItem { Item = item, Subtotal = item.UnitPrice,Quantity = 1 }
                }
                };
                customer = await db.Customers.Include(o => o.Orders).SingleAsync(c => c.Name == customer.Name);
                customer.Orders.Add(order);
                await db.SaveChangesAsync();
            }
        }

        // output the orders for a customer?

    }
}
