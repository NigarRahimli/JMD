﻿// HomeController.cs
using JMD.Data;
using JMD.DTOs;
using JMD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JMD.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Order()
        {
            ViewBag.OrderTypeList = _context.OrderTypes.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult Order(OrderDTO orderDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = new Order
                    {
                        Name = orderDto.Name,
                        Email = orderDto.Email,
                        Telephone = orderDto.Telephone,
                        OrderTypeId = orderDto.OrderTypeId,
                        Message = orderDto.Message
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    // Başarılı bir sipariş durumunda ana sayfaya yönlendir
                    return RedirectToAction("Index");
                }

                // Geçersiz model durumunda sipariş formunu tekrar görüntüle
                ViewBag.OrderTypeList = _context.OrderTypes.ToList();
                return View(orderDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);

                // Hata durumunda sipariş formunu tekrar görüntüle
                ViewBag.OrderTypeList = _context.OrderTypes.ToList();
                return View(orderDto);
            }
        }

    }
}