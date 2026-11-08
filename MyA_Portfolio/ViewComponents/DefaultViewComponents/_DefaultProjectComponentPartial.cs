using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var query = _context.Projects.Include(p => p.Category).ToList();
            return View(query);
        }
    }
}
