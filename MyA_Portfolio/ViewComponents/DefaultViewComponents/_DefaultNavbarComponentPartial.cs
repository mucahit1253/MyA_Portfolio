using Microsoft.AspNetCore.Mvc;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
