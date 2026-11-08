using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Areas.Admin.Models;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences
                .Include(e => e.Skills)
                .OrderByDescending(e => e.ExperienceId)
                .ToList();

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            ViewBag.SkillGroups = _context.SkillGroups.Include(sg => sg.Skills).ToList();
            return View(new ExperienceViewModel());
        }

        [HttpPost]
        public IActionResult CreateExperience(ExperienceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SkillGroups = _context.SkillGroups.Include(sg => sg.Skills).ToList();
                return View(model);
            }

            var experience = new Experience
            {
                Title = model.Title,
                CompanyName = model.CompanyName,
                Description = model.Description,
                SideTitle = model.SideTitle,
                Duration = model.Duration,
                WorkType = model.WorkType,
                IsActive = model.IsActive
            };

            if (model.SelectedSkillIds.Any())
            {
                experience.Skills = _context.Skills
                    .Where(s => model.SelectedSkillIds.Contains(s.SkillId))
                    .ToList();
            }

            _context.Experiences.Add(experience);
            _context.SaveChanges();

            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var experience = _context.Experiences
                .Include(e => e.Skills)
                .FirstOrDefault(e => e.ExperienceId == id);

            if (experience == null) return NotFound();

            var model = new ExperienceViewModel
            {
                ExperienceId = experience.ExperienceId,
                Title = experience.Title,
                CompanyName = experience.CompanyName,
                Description = experience.Description,
                SideTitle = experience.SideTitle,
                Duration = experience.Duration,
                WorkType = experience.WorkType,
                IsActive = experience.IsActive,
                SelectedSkillIds = experience.Skills.Select(s => s.SkillId).ToList()
            };

            ViewBag.SkillGroups = _context.SkillGroups.Include(sg => sg.Skills).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateExperience(ExperienceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SkillGroups = _context.SkillGroups.Include(sg => sg.Skills).ToList();
                return View(model);
            }

            var experience = _context.Experiences
                .Include(e => e.Skills)
                .FirstOrDefault(e => e.ExperienceId == model.ExperienceId);

            if (experience == null) return NotFound();

            experience.Title = model.Title;
            experience.CompanyName = model.CompanyName;
            experience.Description = model.Description;
            experience.SideTitle = model.SideTitle;
            experience.Duration = model.Duration;
            experience.WorkType = model.WorkType;
            experience.IsActive = model.IsActive;

            experience.Skills.Clear();
            if (model.SelectedSkillIds.Any())
            {
                var skills = _context.Skills
                    .Where(s => model.SelectedSkillIds.Contains(s.SkillId))
                    .ToList();

                foreach (var skill in skills)
                {
                    experience.Skills.Add(skill);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }

        [HttpPost]
        public IActionResult DeleteExperience(int id)
        {
            var experience = _context.Experiences.FirstOrDefault(e => e.ExperienceId == id);
            if (experience == null) return NotFound();

            _context.Experiences.Remove(experience);
            _context.SaveChanges();

            return RedirectToAction("ExperienceList");
        }
    }
}
