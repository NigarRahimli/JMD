﻿using JMD.DTOs;
using JMD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JMD.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContext;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (findUser == null)
            {
                return RedirectToAction("Login");
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(findUser, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                string cont = _httpContext.HttpContext.Request.Query["controller"].ToString();
                string act = _httpContext.HttpContext.Request.Query["action"].ToString();
                string id = _httpContext.HttpContext.Request.Query["id"].ToString();
                if (!String.IsNullOrWhiteSpace(cont))
                {
                    return RedirectToAction(act, cont, new { Id = id });
                }

                return RedirectToAction("Index", "Home");
            }
            return View(loginDTO);
        }

        public IActionResult Register()
        {
            return View();
        }
     
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                User user = new()
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
                await _userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    string cont = _httpContext.HttpContext.Request.Query["controller"].ToString();
                    string act = _httpContext.HttpContext.Request.Query["action"].ToString();
                    string id = _httpContext.HttpContext.Request.Query["id"].ToString();
                    if (!String.IsNullOrWhiteSpace(cont))
                    {
                        return RedirectToAction(act, cont, new { Id = id });
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(registerDTO);
                }
            }
            catch (Exception)
            {
                return View(registerDTO);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
