using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyA_Portfolio.Models;

namespace MyA_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultMessageComponentPartial : ViewComponent
    {
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public _DefaultMessageComponentPartial(ITempDataDictionaryFactory tempDataFactory)
        {
            _tempDataFactory = tempDataFactory;
        }

        public IViewComponentResult Invoke()
        {
            var tempData = _tempDataFactory.GetTempData(HttpContext);

            var model = new MessageViewModel
            {
                NameSurname = tempData["NameSurname"] as string,
                Email = tempData["Email"] as string,
                Subject = tempData["Subject"] as string,
                MessageDetail = tempData["MessageDetail"] as string
            };

            ViewBag.ContactError = tempData["ContactError"] as string;
            ViewBag.ContactSuccess = tempData["ContactSuccess"] as string;

            return View(model);
        }
    }
}
