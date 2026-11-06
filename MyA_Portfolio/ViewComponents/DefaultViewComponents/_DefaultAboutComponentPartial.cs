using Microsoft.AspNetCore.Mvc;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
