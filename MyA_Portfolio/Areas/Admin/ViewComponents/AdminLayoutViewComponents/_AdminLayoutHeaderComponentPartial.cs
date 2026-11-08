using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _AdminLayoutHeaderComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var unreadCount = _context.Messages.Count(m => m.IsRead == false);
            return View(unreadCount);
        }
    }
}
