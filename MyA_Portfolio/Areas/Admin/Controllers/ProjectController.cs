using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long _maxFileSize = 3 * 1024 * 1024;

        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ProjectList()
        {
            var query = _context.Projects.Include(p => p.Category).ToList();
            return View(query);
        }
        public IActionResult ProjectCreat()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(new ProjectViewModel());
        }
        [HttpPost]
        public IActionResult ProjectCreat(ProjectViewModel model)
        {

            var ext = Path.GetExtension(model.CoverImageFile.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(ext))
            {
                ModelState.AddModelError("CoverImageFile", "Sadece JPG, PNG veya WEBP dosyası yükleyebilirsiniz.");
            }
            else if (model.CoverImageFile.Length > _maxFileSize)
            {
                ModelState.AddModelError("CoverImageFile", "Dosya boyutu 3 MB'ı geçemez.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return View(model);
            }
            // Benzersiz dosya adı üret (orijinal adı asla güvenmeyiz)
            var fileExt = Path.GetExtension(model.CoverImageFile!.FileName).ToLowerInvariant();
            var uniqueFileName = $"{Guid.NewGuid()}{fileExt}";

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "projects");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.CoverImageFile.CopyTo(stream);
            }


            var project = new Project()
            {
                Title = model.Title,
                Description = model.Description,
                CoverImageUrl = $"/uploads/projects/{uniqueFileName}",
                GithubUrl = model.GithubUrl,
                CategoryId = model.CategoryId!.Value

            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }
        [HttpGet]
        public IActionResult ProjectUpdate(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null) return NotFound();

            var model = new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
                Description = project.Description,
                GithubUrl = project.GithubUrl,
                CoverImageUrl = project.CoverImageUrl,
                CategoryId = project.CategoryId
            };

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(model);
        }

        [HttpPost]
        public IActionResult ProjectUpdate(ProjectViewModel model)
        {
            // Dosya seçilmediyse, boyut/uzantı kontrolünü atla (mevcut görsel korunacak)
            if (model.CoverImageFile != null)
            {
                var ext = Path.GetExtension(model.CoverImageFile.FileName).ToLowerInvariant();

                if (!_allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("CoverImageFile", "Sadece JPG, PNG veya WEBP dosyası yükleyebilirsiniz.");
                }
                else if (model.CoverImageFile.Length > _maxFileSize)
                {
                    ModelState.AddModelError("CoverImageFile", "Dosya boyutu 3 MB'ı geçemez.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return View(model);
            }

            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == model.ProjectId);
            if (project == null) return NotFound();

            project.Title = model.Title;
            project.Description = model.Description;
            project.GithubUrl = model.GithubUrl;
            project.CategoryId = model.CategoryId!.Value;

            // Yeni dosya seçildiyse: yükle, eski dosyayı sil, yolu güncelle
            if (model.CoverImageFile != null)
            {
                var fileExt = Path.GetExtension(model.CoverImageFile.FileName).ToLowerInvariant();
                var uniqueFileName = $"{Guid.NewGuid()}{fileExt}";

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "projects");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverImageFile.CopyTo(stream);
                }

                // Eski dosyayı diskten sil (varsa)
                if (!string.IsNullOrEmpty(project.CoverImageUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", project.CoverImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                project.CoverImageUrl = $"/uploads/projects/{uniqueFileName}";
            }
            // Dosya seçilmediyse, project.CoverImageUrl'e hiç dokunmuyoruz — eski değeri koruyor

            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }

        public IActionResult ProjectDelete(int id)
        {
            var project=_context.Projects.FirstOrDefault(p => p.ProjectId == id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }



    }
}
