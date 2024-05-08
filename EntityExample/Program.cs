using CodeFirst.Models;

namespace CodeFirst
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            await GetAllItems();
            await AddNewItem();
            await GetAllItems();
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
    }
}
