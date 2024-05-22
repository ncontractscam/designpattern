using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScaffoldingImport.Models;

namespace Entity.Tests
{
    public static class TestFactory
    {
        private static Faker _faker;
        static TestFactory()
        {
            _faker = new Faker();
        }

        public static Customer CreateCustomer(string? name = null, string? address = null)
        {
            name ??= _faker.Name.FirstName();
            address ??= _faker.Address.StreetAddress();
            return new Customer { Name = name, Address = address };
        }

        public static Item CreateItem(decimal price = 0, string? name = null)
        {
            name ??= _faker.Music.Genre();
            price = price == 0 ? _faker.Finance.Amount() : price;
            return new Item
            {
                Name = name,
                UnitPrice = price
            };
        }
    }
}
