using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeadComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeadComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }
    }
}
