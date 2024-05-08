namespace CodeFirst.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public virtual List<Order> Orders { get; set; }
        
    }
}
