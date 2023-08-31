using JMD.Data;
using JMD.DTOs;
using JMD.Models;
using JMD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JMD.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var ordersForDashboard = GetOrdersForDashboard();
            return View(ordersForDashboard);
        }
        public List<OrderDashboardVM> GetOrdersForDashboard()
        {
            List<Order> orders = _context.Orders.Include(x=>x.OrderType).ToList();
            List<OrderDashboardVM> orderDashboardVMs = orders.Select(order => new OrderDashboardVM
            {
                Name = order.Name,
                Email = order.Email,
                Telephone = order.Telephone,
                OrderTypeName = order.OrderType.OrderName,
                Message = order.Message
            }).ToList();

            return orderDashboardVMs;
        }

        public IActionResult OrderType()
        {
            return View();

        }
        [HttpPost]
        public IActionResult OrderType(OrderTypeDTO orderTypeDTO)
        {
            OrderType orderType = new()
            {
                OrderName=orderTypeDTO.OrderName
            };
            _context.OrderTypes.Add(orderType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
