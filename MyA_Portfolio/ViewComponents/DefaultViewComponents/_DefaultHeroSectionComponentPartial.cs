using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var hero = _context.HeroSections.FirstOrDefault(); // tek satırlık kayıt
            var contact = _context.Contacts.FirstOrDefault();

            ViewBag.Hero = hero;
            ViewBag.Contact = contact;
            return View();
        }
    }
}
