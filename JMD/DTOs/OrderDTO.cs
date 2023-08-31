using JMD.Models;

namespace JMD.DTOs
{
    public class OrderDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int OrderTypeId { get; set; }
        public string Message { get; set; }


    }
}
