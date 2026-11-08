using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Areas.Admin.Models;
using MyA_Portfolio.Context;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayoutController : Controller
    {
        

        

        public IActionResult Index()
        {
           
            return View();
        }
    }
}
