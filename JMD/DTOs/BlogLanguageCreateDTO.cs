namespace JMD.DTOs
{
    public class BlogLanguageCreateDTO
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string LanguageCode { get; set; }
        public bool IsFeatured { get; set; }
    }
}
