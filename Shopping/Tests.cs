using NUnit.Framework;
using Shopping.CodingChallenge;

namespace Shopping.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ChristmasShopping()
        {
            var result = Shop.ChristmasShoppingAtTheGroceryStore();
            Assert.That(result[0], Is.EqualTo(348.90));
            Assert.That(result[1], Is.EqualTo(34.89));
        }

        [Test]
        public void BuyingFood()
        {
            var result = Shop.BuyingFood();
            Assert.That(result[0], Is.EqualTo(74.5483));
            Assert.That(result[1], Is.EqualTo(67.09347));
        }
    }
}