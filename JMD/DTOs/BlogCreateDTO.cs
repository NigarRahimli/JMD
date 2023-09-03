namespace JMD.DTOs
{
    public class BlogCreateDTO
    {
        public  List<BlogLanguageCreateDTO> BlogLanguages { get; set; }
        public string PhotoUrl { get; set; }

    }
}
