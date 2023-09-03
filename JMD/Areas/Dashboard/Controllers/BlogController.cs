using System;
using System.Collections.Generic;
using System.IO;
using JMD.Data;
using JMD.DTOs;
using JMD.Helpers.SeoHelper;
using JMD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMD.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogCreateDTO blogCreateDTO, IFormFile Photourl)
        {
            try
            {
                if (Photourl != null && Photourl.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(Photourl.FileName);
                    var path = Path.Combine(_env.WebRootPath, "uploads", "Blog", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        Photourl.CopyTo(fileStream);
                    }

                    blogCreateDTO.PhotoUrl = "/uploads/Blog/" + fileName;
                }

                var blog = new Blog
                {
                    PhotoUrl = blogCreateDTO.PhotoUrl,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    BlogLangs = new List<BlogLanguage>()
                };

                _appDbContext.Blogs.Add(blog);
                _appDbContext.SaveChanges();

                var blogId = blog.Id;

                var blogLanguages = new List<BlogLanguage>();
                var langCodes = new[] { "az-AZ", "en-US" };
                for (int i = 0; i < langCodes.Length; i++)
                {
                    var langCode = langCodes[i];
                    var languageDTO = blogCreateDTO.BlogLanguages[i];
                    var blogLanguage = new BlogLanguage
                    {
                        LanguageCode = langCode,
                        Name = languageDTO.Title,
                        ShortDesc = languageDTO.ShortDesc,
                        Description = languageDTO.Description,
                        SeoUrl = CreateSeo.CreateSeoUrl(languageDTO.Title, langCode),
                        BlogId = blogId,
                        IsFeatured=true

                    };
                    blogLanguages.Add(blogLanguage);
                }

                _appDbContext.BlogLanguages.AddRange(blogLanguages);
                _appDbContext.SaveChanges();

                return View("Index");
            }
            catch (Exception ex)
            {
                return View(blogCreateDTO);
            }
        }
    }
}
