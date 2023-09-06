namespace JMD.DTOs
{
    public class BlogCreateDTO
    {
        public  List<BlogLanguageCreateDTO> BlogLanguages { get; set; }
        public bool IsFavourite { get; set; }
        public string PhotoUrl { get; set; }

    }
}
