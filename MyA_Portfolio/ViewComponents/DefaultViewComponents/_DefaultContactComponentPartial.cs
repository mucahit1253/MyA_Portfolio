using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultContactComponentPartial:ViewComponent
    {

        private readonly PortfolioContext _context;

        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke() 
        {
            var values= _context.Contacts.FirstOrDefault(p=>p.ContactId==1);
            return View(values);
        }
    }
}
