using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;
using MyA_Portfolio.Models;

namespace MyA_Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;

        public DefaultController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
