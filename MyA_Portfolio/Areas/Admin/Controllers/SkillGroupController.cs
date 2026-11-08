using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Areas.Admin.Models;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillGroupController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillGroupController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillGroupList()
        {
            var query = _context.SkillGroups.ToList();
            return View(query);
        }

        [HttpGet]
        public IActionResult SkillGroupCreate()
        {
            return View(new SkillGroupViewModel());
        }

        [HttpPost]
        public IActionResult SkillGroupCreate(SkillGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var skillGroup = new SkillGroup
            {
                Title = model.Title,
                Description = model.Description,
                IconClass = model.IconClass
            };

            _context.SkillGroups.Add(skillGroup);
            _context.SaveChanges();

            return RedirectToAction("SkillGroupList");
        }

        [HttpGet]
        public IActionResult SkillGroupUpdate(int id)
        {
            var skillGroup = _context.SkillGroups.FirstOrDefault(sg => sg.SkillGroupId == id);
            if (skillGroup == null) return NotFound();

            var model = new SkillGroupViewModel
            {
                SkillGroupId = skillGroup.SkillGroupId,
                Title = skillGroup.Title,
                Description = skillGroup.Description,
                IconClass = skillGroup.IconClass
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SkillGroupUpdate(SkillGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var skillGroup = _context.SkillGroups.FirstOrDefault(sg => sg.SkillGroupId == model.SkillGroupId);
            if (skillGroup == null) return NotFound();

            skillGroup.Title = model.Title;
            skillGroup.Description = model.Description;
            skillGroup.IconClass = model.IconClass;

            _context.SaveChanges();
            return RedirectToAction("SkillGroupList");
        }

        [HttpPost]
        public IActionResult SkillGroupDelete(int id)
        {
            var skillGroup = _context.SkillGroups.FirstOrDefault(sg => sg.SkillGroupId == id);
            if (skillGroup == null) return NotFound();

            _context.SkillGroups.Remove(skillGroup);
            _context.SaveChanges();

            return RedirectToAction("SkillGroupList");
        }
    }
}
