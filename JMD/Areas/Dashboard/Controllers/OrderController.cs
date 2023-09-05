using JMD.Data;
using JMD.DTOs;
using JMD.Helpers.Enums;
using JMD.Models;
using JMD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
                OrderId=order.Id,
                Name = order.Name,
                Email = order.Email,
                Telephone = order.Telephone,
                OrderTypeName = order.OrderType.OrderName,
                Message = order.Message,
                Stage=(int)Stage.isNew,
                StageName=order.StageName,
               
            }).ToList();

            return orderDashboardVMs;
        }
        [HttpPost]

        public IActionResult ChangeStage(int orderId, string newStage)
        {
            // Fetch the order from the database based on orderId
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound(); 
            }

         
            if (Enum.TryParse<Stage>(newStage, ignoreCase: true, out var parsedStage))
            {
               
                order.Stage = (int)parsedStage;
                order.StageName= Enum.GetName(typeof(Stage), order.Stage);
                
                _context.SaveChanges();

                return View("Index"); 
            }
            else
            {
                return BadRequest("Invalid newStage value");
            }
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
