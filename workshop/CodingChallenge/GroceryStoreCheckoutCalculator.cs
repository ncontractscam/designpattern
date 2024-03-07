namespace Shopping.CodingChallenge
{
    public class GroceryStoreCheckoutCalculator
    {
        public decimal Calculate(List<CartItem> carts, DateTime checkOutDate)
        {
            decimal itemTotal = 0m;

            foreach (var item in carts)
            {
                if (item.Category == "Christmas")
                {
                    if (checkOutDate.Month == 12)
                    {
                        if (checkOutDate.Day < 15)
                        {
                            itemTotal += item.Quantity * (item.Price - item.Price * (20m / 100m));
                        }
                        else if (checkOutDate.Day <= 25)
                        {
                            itemTotal += item.Quantity * (item.Price - item.Price * (60m / 100m));
                        }
                        else
                        {
                            itemTotal += item.Quantity * (item.Price - item.Price * (90m / 100m));
                        }
                    }
                    else
                    {
                        itemTotal += item.Quantity * item.Price;
                    }
                }
                else if (item.Category == "Food")
                {
                    if (item.Weight != 0)
                    {
                        if (checkOutDate.TimeOfDay.Hours > 6 && checkOutDate.TimeOfDay.Hours <= 8)
                        {
                            //senior discount
                            itemTotal += item.Weight * item.Price * 0.9m;
                        }
                        else
                        {
                            itemTotal += item.Weight * item.Price;
                        }
                    }
                    else
                    {
                        if (checkOutDate.TimeOfDay.Hours > 6 && checkOutDate.TimeOfDay.Hours <= 8)
                        {
                            //senior discount
                            itemTotal += item.Quantity * item.Price * 0.9m;
                        }
                        else
                        {
                            itemTotal += item.Quantity * item.Price;
                        }
                    }
                }
                else if (item.Category == "")
                {
                    //oh no! this should not happen!
                }
                else
                {
                    itemTotal += item.Price * item.Quantity;
                }
            }
            return itemTotal;
        }
    }
}