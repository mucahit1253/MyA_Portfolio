using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;

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
