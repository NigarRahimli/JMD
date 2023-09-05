using JMD.Data;
using JMD.DTOs;
using JMD.Models;
using JMD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JMD.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeBlogVM homeBlogVM = new HomeBlogVM()
            {
                FavouriteBlogs = GetFavourite("az-AZ"),
                RecentBlogs=GetRecent("en-US")
            };
            return View(homeBlogVM);
        }
        private List<FavouriteBlogDTO> GetFavourite(string langcode)
        {
            var results = _context.BlogLanguages.Include(x=>x.Blog)
                .Where(x => x.LanguageCode == langcode &&
                            x.Blog.IsFavorite == true &&
                            x.Blog.IsDeleted == false).Take(3)
                .Select(x => new FavouriteBlogDTO
                {
                    PhotoUrl = x.Blog.PhotoUrl,
                    Name = x.Name,
                    Description = x.Description,
                    ShortDesc = x.ShortDesc
                })
                .ToList();

            return results;
        }
        private List<RecentBlogDTO> GetRecent(string langcode)
        {
            var results = _context.BlogLanguages
                .Where(x => x.LanguageCode == langcode &&
                            x.Blog.IsDeleted == false).Take(8)
                .Select(x => new RecentBlogDTO
                {
                    PhotoUrl = x.Blog.PhotoUrl,
                    Name = x.Name,
                    Description = x.Description,
                    ShortDesc = x.ShortDesc
                })
                .ToList();

            return results;
        }
    }

}

