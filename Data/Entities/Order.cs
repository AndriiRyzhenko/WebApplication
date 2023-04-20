namespace Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public ICollection<OrderedFood> OrderedFood { get; set; }
    }
}
