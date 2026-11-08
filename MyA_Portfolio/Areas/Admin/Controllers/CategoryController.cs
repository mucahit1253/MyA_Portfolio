using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Areas.Admin.Models;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var query = _context.Categories.ToList();

            return View(query);
        }
        public IActionResult CategoryUpdate(int id)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == model.CategoryId);
            if (category == null)
                return NotFound();
            category.CategoryName = model.CategoryName;
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        public IActionResult CategoryCreat()
        {

            return View(new CategoryViewModel());
        }
        [HttpPost]
        public IActionResult CategoryCreat(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = new Category
            {
                CategoryName = model.CategoryName
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            
            return RedirectToAction("CategoryList");
        }
        public IActionResult CategoryDelete(int id)
        {
            var query = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            _context.Categories.Remove(query);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}
