using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList(string? search, string? sortOrder)
        {
            var query = _context.Messages.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NameSurname.Contains(search)
                                     || p.Subject.Contains(search)
                                     || p.Email.Contains(search));
            }
            query = sortOrder switch
            {
                "oldest" => query.OrderBy(m => m.SendDate),
                _ => query.OrderByDescending(p => p.SendDate)
            };
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sortOrder;

            var values = query.ToList();
            return View(values);
           
        }
        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            var values = _context.Messages.Find(id);
            _context.Messages.Remove(values);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == id);
            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }
            return Ok();
        }

        public IActionResult DetailMessage(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == id);
            if (message == null) return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return View(message);
        }
    }
}
