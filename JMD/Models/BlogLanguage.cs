namespace JMD.Models
{
    public class BlogLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        public string LanguageCode { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SeoUrl { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
