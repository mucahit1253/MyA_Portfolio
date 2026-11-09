using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Areas.Admin.Models;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;
using MyA_Portfolio.Models;

namespace MyA_Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;
        public DefaultController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.Setting = _context.Settings.FirstOrDefault();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.Contact = _context.Contacts.FirstOrDefault();
          
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ContactError"] = "Lütfen formu eksiksiz ve doğru doldurun.";
                TempData["NameSurname"] = model.NameSurname;
                TempData["Email"] = model.Email;
                TempData["Subject"] = model.Subject;
                TempData["MessageDetail"] = model.MessageDetail;
                return RedirectToAction("Index", "Default", null, "contact");
            }

            var message = new Message
            {
                NameSurname = model.NameSurname,
                Email = model.Email,
                Subject = model.Subject,
                MessageDetail = model.MessageDetail,
                SendDate = DateTime.Now,
                IsRead = false
            };
            _context.Messages.Add(message);
            _context.SaveChanges();

            TempData["ContactSuccess"] = "Mesajınız başarıyla iletildi! Teşekkür ederiz.";
            return RedirectToAction("Index", "Default", null, "contact");
        }

        public IActionResult DownloadCv()
        {
            var about = _context.Abouts.FirstOrDefault();
            if (about == null || string.IsNullOrEmpty(about.CvFileUrl))
                return NotFound();

            var fullPath = Path.Combine(_env.WebRootPath, about.CvFileUrl.TrimStart('/'));
            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(fullPath);
            return File(bytes, "application/pdf", "CV.pdf");
        }
    }
}
