namespace JMD.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }
        public string Message { get; set; }

    }
}
