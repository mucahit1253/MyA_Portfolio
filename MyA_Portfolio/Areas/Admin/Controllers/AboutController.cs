using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;
using MyA_Portfolio.Areas.Admin.Models;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAbout(AboutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var about = new About
            {
                NameSurname = model.NameSurname,
                JobTitle = model.JobTitle,
                Location = model.Location,
                Expert = model.Expert,
                Email = model.Email,
                Description = model.Description,
                OpenToWork = model.OpenToWork
            };

            if (model.CvFile != null)
            {
                var extension = Path.GetExtension(model.CvFile.FileName).ToLower();
                if (extension != ".pdf")
                {
                    ModelState.AddModelError("", "Sadece PDF dosyası yükleyebilirsiniz.");
                    return View(model);
                }

                var cvFolder = Path.Combine(_env.WebRootPath, "cv");
                if (!Directory.Exists(cvFolder))
                    Directory.CreateDirectory(cvFolder);

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(cvFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.CvFile.CopyTo(stream);
                }

                about.CvFileUrl = "/cv/" + fileName;
            }

            _context.Abouts.Add(about);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }

        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            _context.Abouts.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var about = _context.Abouts.Find(id);
            if (about == null)
                return NotFound();

            var model = new AboutViewModel
            {
                AboutId = about.AboutId,
                NameSurname = about.NameSurname,
                JobTitle = about.JobTitle,
                Location = about.Location,
                Expert = about.Expert,
                Email = about.Email,
                Description = about.Description,
                OpenToWork = about.OpenToWork,
                CvFileUrl = about.CvFileUrl
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateAbout(AboutViewModel model)
        {
            var about = _context.Abouts.Find(model.AboutId);
            if (about == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                model.CvFileUrl = about.CvFileUrl;
                return View(model);
            }

            about.NameSurname = model.NameSurname;
            about.JobTitle = model.JobTitle;
            about.Location = model.Location;
            about.Expert = model.Expert;
            about.Email = model.Email;
            about.Description = model.Description;
            about.OpenToWork = model.OpenToWork;

            if (model.CvFile != null)
            {
                var extension = Path.GetExtension(model.CvFile.FileName).ToLower();
                if (extension != ".pdf")
                {
                    ModelState.AddModelError("", "Sadece PDF dosyası yükleyebilirsiniz.");
                    model.CvFileUrl = about.CvFileUrl;
                    return View(model);
                }

                if (!string.IsNullOrEmpty(about.CvFileUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, about.CvFileUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var cvFolder = Path.Combine(_env.WebRootPath, "cv");
                if (!Directory.Exists(cvFolder))
                    Directory.CreateDirectory(cvFolder);

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(cvFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.CvFile.CopyTo(stream);
                }

                about.CvFileUrl = "/cv/" + fileName;
            }

            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }
    }
}
