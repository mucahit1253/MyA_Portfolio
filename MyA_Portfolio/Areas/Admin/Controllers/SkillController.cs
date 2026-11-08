using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            var skills = _context.Skills
                .Include(s => s.SkillGroup)
                .ToList();

            return View(skills);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            ViewBag.SkillGroups = new SelectList(_context.SkillGroups, "SkillGroupId", "Title");
            return View(new SkillViewModel());
        }

        [HttpPost]
        public IActionResult CreateSkill(SkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SkillGroups = new SelectList(_context.SkillGroups, "SkillGroupId", "Title");
                return View(model);
            }

            var skill = new Skill
            {
                Name = model.Name,
                SkillGroupId = model.SkillGroupId!.Value
            };

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return RedirectToAction("SkillList");
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == id);
            if (skill == null) return NotFound();

            var model = new SkillViewModel
            {
                SkillId = skill.SkillId,
                Name = skill.Name,
                SkillGroupId = skill.SkillGroupId
            };

            ViewBag.SkillGroups = new SelectList(_context.SkillGroups, "SkillGroupId", "Title");
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateSkill(SkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SkillGroups = new SelectList(_context.SkillGroups, "SkillGroupId", "Title");
                return View(model);
            }

            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == model.SkillId);
            if (skill == null) return NotFound();

            skill.Name = model.Name;
            skill.SkillGroupId = model.SkillGroupId!.Value;

            _context.SaveChanges();
            return RedirectToAction("SkillList");
        }

        [HttpPost]
        public IActionResult DeleteSkill(int id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == id);
            if (skill == null) return NotFound();

            _context.Skills.Remove(skill);
            _context.SaveChanges();

            return RedirectToAction("SkillList");
        }
    }
}
