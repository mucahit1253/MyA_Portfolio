using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultScrollingComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultScrollingComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Skills.ToList();
            return View(values);
        }
    }
}
