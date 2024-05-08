﻿using CodeFirst.Models;
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

            while(customer == null)
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

        public static async Task AddNewOrder(Customer customer)
        {
            Console.WriteLine("Enter a name for a new order: ");
            decimal total = 0;
            var name = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter the total: ");
            } while (!decimal.TryParse(Console.ReadLine(), out total));

            Console.WriteLine($"Adding {name} with total ${total}");
            var order = new Order { Name = name, Total = total };


            await using (var db = new ShoppingContext())
            {
                customer = await db.Customers.Include(o => o.Orders).SingleAsync(c => c.Name == customer.Name);
                customer.Orders.Add(order);
                await db.SaveChangesAsync();
            }
        }
    }
}
