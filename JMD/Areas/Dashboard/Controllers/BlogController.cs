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
using Microsoft.EntityFrameworkCore;

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
         
            var blogs = _appDbContext.BlogLanguages.Include(x=>x.Blog).ToList();
            return View(blogs); 
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
            IsFavorite=blogCreateDTO.IsFavourite,
            IsDeleted = false,

            BlogLangs = new List<BlogLanguage>()
        };

        _appDbContext.Blogs.Add(blog);
        _appDbContext.SaveChanges();

        var blogId = blog.Id;

        if (!string.IsNullOrEmpty(blogCreateDTO.BlogLanguages[0].Title))
        {
            var azLanguageDTO = blogCreateDTO.BlogLanguages[0];
            var azBlogLanguage = new BlogLanguage
            {
                LanguageCode = "az-AZ", 
                Name = azLanguageDTO.Title,
                ShortDesc = azLanguageDTO.ShortDesc,
                Description = azLanguageDTO.Description,
                SeoUrl = CreateSeo.CreateSeoUrl(azLanguageDTO.Title, "az-AZ"),
                BlogId = blogId,
                
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            _appDbContext.BlogLanguages.Add(azBlogLanguage);
        }

        if (!string.IsNullOrEmpty(blogCreateDTO.BlogLanguages[1].Title))
        {
            var enLanguageDTO = blogCreateDTO.BlogLanguages[1];
            var enBlogLanguage = new BlogLanguage
            {
                LanguageCode = "en-US", 
                Name = enLanguageDTO.Title,
                ShortDesc = enLanguageDTO.ShortDesc,
                Description = enLanguageDTO.Description,
                SeoUrl = CreateSeo.CreateSeoUrl(enLanguageDTO.Title, "en-US"),
                BlogId = blogId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _appDbContext.BlogLanguages.Add(enBlogLanguage);
        }

        _appDbContext.SaveChanges();

       
        return RedirectToAction("Index"); 
    }
    catch (Exception ex)
    {
        return View(blogCreateDTO);
    }
}
        public IActionResult Edit(int id)
        {
            try
            {
                var blog = _appDbContext.Blogs.Include(x => x.BlogLangs).FirstOrDefault(x => x.Id == id);

                if (blog == null)
                {
                    return NotFound();
                }

                var blogUpdateDTO = new BlogUpdateDTO
                {
                    IsDeleted = blog.IsDeleted,
                    IsFavourite = blog.IsFavorite,
                    BlogLanguages = blog.BlogLangs.Select(bl => new BlogLangUpdateDTO
                    {
                        Title = bl.Name,
                        ShortDesc = bl.ShortDesc,
                        Description = bl.Description,
                        LangCode=bl.LanguageCode
                        
                    }).ToList()
                };

                return View(blogUpdateDTO);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Edit(int id, BlogUpdateDTO blogUpdateDTO, IFormFile Photourl)
        {
            try
            {
                var blog = _appDbContext.Blogs.Include(x => x.BlogLangs).FirstOrDefault(x => x.Id == id);

                if (blog == null)
                {
                    return NotFound();
                }

                blog.IsDeleted = blogUpdateDTO.IsDeleted;
                blog.IsFavorite = blogUpdateDTO.IsFavourite;

                if (Photourl != null && Photourl.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(Photourl.FileName);
                    var path = Path.Combine(_env.WebRootPath, "uploads", "Blog", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        Photourl.CopyTo(fileStream);

                        if (!string.IsNullOrEmpty(blog.PhotoUrl))
                        {
                            var oldFilePath = Path.Combine(_env.WebRootPath, blog.PhotoUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        blog.PhotoUrl = "/uploads/Blog/" + fileName;
                    }
                }

                for (int i = 0; i < blog.BlogLangs.Count; i++)
                {
                    blog.BlogLangs[i].ShortDesc = blogUpdateDTO.BlogLanguages[i].ShortDesc;
                    blog.BlogLangs[i].Description = blogUpdateDTO.BlogLanguages[i].Description;
                    blog.BlogLangs[i].Name = blogUpdateDTO.BlogLanguages[i].Title;
                    blog.BlogLangs[i].UpdatedDate = DateTime.Now;
                    blog.BlogLangs[i].SeoUrl = CreateSeo.CreateSeoUrl(blogUpdateDTO.BlogLanguages[i].Title, "az-AZ");
                }

                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(blogUpdateDTO);
            }
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var blog = _appDbContext.Blogs.Include(b => b.BlogLangs).FirstOrDefault(b => b.Id == id);

                if (blog != null)
                {
                    blog.IsDeleted= true;
                    _appDbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


    }
}
