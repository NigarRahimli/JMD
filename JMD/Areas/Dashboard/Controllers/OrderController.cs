using JMD.Data;
using JMD.DTOs;
using JMD.Helpers.Enums;
using JMD.Models;
using JMD.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JMD.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult ChangeStage(int orderId, Stage newStage)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.Stage = (int)newStage;
            order.StageName = Enum.GetName(typeof(Stage), newStage);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult OrderType()
        {
            ViewBag.orderTypes = _context.OrderTypes.Where(x=>x.IsDeleted==false).ToList();
            return View();

        }


        public IActionResult Detail(int id)
        {
            var result = _context.Orders.Include(x=>x.OrderType).FirstOrDefault(x => x.Id == id);
            return View(result);
        }
       


        [HttpPost]
        public IActionResult OrderType(OrderTypeDTO orderTypeDTO)
        {
            OrderType orderType = new()
            {
                OrderName=orderTypeDTO.OrderName,
                IsDeleted=false
            };
            _context.OrderTypes.Add(orderType);
            _context.SaveChanges();
            return RedirectToAction("OrderType");
        }
        [HttpPost]
        public IActionResult  TypeDelete(int id)
        {
            var orderType =  _context.OrderTypes.FirstOrDefault(x=>x.Id==id);

        
                orderType.IsDeleted= true;
                _context.SaveChanges();

                return RedirectToAction("OrderType");
            
      
         
        }



    }

}
