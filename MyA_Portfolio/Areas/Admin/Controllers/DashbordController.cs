using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Areas.Admin.Models;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashbordController : Controller
    {
        private readonly PortfolioContext _context;

        public DashbordController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                AboutCount = _context.Abouts.Count(),
                EducationCount = _context.Educations.Count(),
                ExperienceCount = _context.Experiences.Count(),
                MessageCount = _context.Messages.Count(),
                CategoryCount=_context.Categories.Count(),
                ProjeCount=_context.Projects.Count(),
                UnreadMessageCount = _context.Messages.Count(m => !m.IsRead),
                RecentMessages = _context.Messages
              .OrderByDescending(m => m.SendDate)
              .Take(8)
           .Select(m => new RecentMessageVm
           {
               MessageId = m.MessageId,
               SenderName = m.NameSurname,
               Subject = m.Subject,
               SentDate = m.SendDate,
               IsRead = m.IsRead,
               Body=m.MessageDetail
           })
           .ToList(),
                ProjectsByCategory = _context.Categories
            .Select(c => new CategoryProjectCountVm
            {
                CategoryName = c.CategoryName,
                ProjectCount = _context.Projects.Count(p => p.CategoryId == c.CategoryId)
            })
            .ToList()
            };
            return View(model);
        }
        
    }
}
