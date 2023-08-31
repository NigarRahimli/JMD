namespace JMD.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public List<BlogLanguage> BlogLangs { get; set;}
    }
}
