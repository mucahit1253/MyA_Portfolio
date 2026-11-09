using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.SkillGroups.Include(p => p.Skills).ToList();
            return View(values);
        }
    }
}
