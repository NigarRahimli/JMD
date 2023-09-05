using JMD.DTOs;

namespace JMD.ViewModels
{
    public class HomeBlogVM
    {
    public List<FavouriteBlogDTO> FavouriteBlogs { get; set; }
        public List<RecentBlogDTO> RecentBlogs { get; set; }
    }
}
