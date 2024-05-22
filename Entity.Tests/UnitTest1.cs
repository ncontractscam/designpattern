using Bogus;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ScaffoldingImport;
using ScaffoldingImport.Models;

namespace Entity.Tests
{
    public class Tests
    {
        private Faker _faker;

        public Tests()
        {
            _faker = new Faker();
        }


        [OneTimeSetUp]
        public async Task Setup()
        {
            await Cleanup();

            var customers = Enumerable.Range(0, 20).Select(x => TestFactory.CreateCustomer());
            var items = Enumerable.Range(0, 20).Select(x => TestFactory.CreateItem());

            await using var shoppingContext = new ShoppingContext();
            shoppingContext.Customers.AddRange(customers);
            shoppingContext.Items.AddRange(items);
            await shoppingContext.SaveChangesAsync();

            //Assertions in setup are only useful for integration level tests
            var customerCount = await shoppingContext.Customers.CountAsync();
            var itemCount = await shoppingContext.Items.CountAsync();
            
            Assert.That(customerCount, Is.EqualTo(20));
            Assert.That(itemCount, Is.EqualTo(20));
        }

        [Test]
        public async Task InitializedTest()
        {

        }


        //doing cleanup within setup to allow us to query databases after, generally when doing Integration Tests like this, you want to leave the database clean
        //[OneTimeTearDown]
        public async Task Cleanup()
        {
            await using (var shoppingContext = new ShoppingContext())
            {
                await shoppingContext.OrderItems.ExecuteDeleteAsync();
                await shoppingContext.Orders.ExecuteDeleteAsync();
                await shoppingContext.Customers.ExecuteDeleteAsync();
                await shoppingContext.Items.ExecuteDeleteAsync();
            }
        }
    }
}