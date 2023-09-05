namespace JMD.ViewModels
{
    public class OrderDashboardVM
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string OrderTypeName { get; set; }
        public string Message { get; set; }
        public int Stage { get; set; }
        public string StageName { get; set; }
    }
}
