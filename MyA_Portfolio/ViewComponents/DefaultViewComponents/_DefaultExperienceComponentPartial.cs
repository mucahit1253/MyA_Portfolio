using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Experiences.Include(p=>p.Skills).Where(p=>p.IsActive).ToList();
            
            return View(values);
        }
    }
}
