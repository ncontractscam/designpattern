using Bogus;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ScaffoldingImport;
using ScaffoldingImport.Models;

namespace Entity.Tests
{
    public class Tests
    {
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
            //Arrange
            //Setup A Order on a customer
            await using (var shoppingContext = new ShoppingContext())
            {
                var existingItem = await shoppingContext.Items.FirstOrDefaultAsync();
                var existing = await shoppingContext.Customers.FirstOrDefaultAsync();
                var order = new Order
                {
                    Customer = existing,
                    Name = "MyName",
                    Total = 10,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ItemGuid = existingItem.Guid,
                            Quantity = 3,
                            Subtotal = 3 * existingItem.UnitPrice
                        }
                    }
                };
                shoppingContext.Orders.Add(order);
                await shoppingContext.SaveChangesAsync();
            }

            //Act

            await using (var shoppingContext = new ShoppingContext())
            {
                var result = await shoppingContext.OrderItems.FirstOrDefaultAsync();
                Assert.That(result, Is.Not.Null);
            }


            //Assert
            //Cameron bets there will be no order on order item
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