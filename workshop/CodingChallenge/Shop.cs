namespace Shopping.CodingChallenge
{
    public static class Shop
    {
        public static List<decimal> ChristmasShoppingAtTheGroceryStore()
        {
            var values = new List<decimal>();
            var carts = new List<CartItem>
            {
                new CartItem {ProductName = "Lights", Category = "Christmas", Price = 5.99m, Quantity = 10},
                new CartItem {ProductName = "Tree", Category = "Christmas", Price = 169m, Quantity = 1},
                new CartItem {ProductName = "Ornaments", Category = "Christmas", Price = 8m, Quantity = 15},
            };

            var calculator = new GroceryStoreCheckoutCalculator();
            var total = calculator.Calculate(carts, new DateTime(2020, 11, 30));
            values.Add(total);

            var totalAfterChristmas = calculator.Calculate(carts, new DateTime(2020, 12, 30));
            values.Add(totalAfterChristmas);
            return values;  
        }

        public static List<decimal> BuyingFood()
        {
            var values = new List<decimal>();
            var carts = new List<CartItem>
            {
                new CartItem {ProductName = "Apple", Category = "Food", Price = 3.27m, Weight = 0.79m},
                new CartItem {ProductName = "Scallop", Category = "Food", Price = 18m, Weight = 1.5m},
                new CartItem {ProductName = "Salad", Category = "Food", Price = 6.99m, Quantity = 1},
                new CartItem {ProductName = "Ground Beef", Category = "Food", Price = 7.99m, Weight = 1.5m},
                new CartItem {ProductName = "Red Wine", Category = "Food", Price = 25.99m, Quantity = 1}
            };

            var calculator = new GroceryStoreCheckoutCalculator();
            var total = calculator.Calculate(carts, new DateTime(2020, 11, 30));
            values.Add(total);

            var seniorHourTotal = calculator.Calculate(carts, new DateTime(2020, 11, 30, 7, 11, 0));
            values.Add(seniorHourTotal);
            return values;
        }

    }
}