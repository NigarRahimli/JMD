namespace JMD.DTOs
{
    public class BlogUpdateDTO
    {
        public string PhotoUrl { get; set; }

        public List<BlogLangUpdateDTO> BlogLanguages { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFavourite { get; set; }
    }
}
