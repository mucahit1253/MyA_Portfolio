using Microsoft.AspNetCore.Mvc;
using MyA_Portfolio.Context;
using MyA_Portfolio.Entities;
using MyA_Portfolio.Areas.Admin.Models;

namespace MyA_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Settings()
        {
            var setting = _context.Settings.FirstOrDefault();

            var model = new SettingViewModel();
            if (setting != null)
            {
                model.SettingId = setting.SettingId;
                model.SiteTitle = setting.SiteTitle;
                model.MetaDescription = setting.MetaDescription;
                model.FooterText = setting.FooterText;
                model.LogoUrl = setting.LogoUrl;
                model.FaviconUrl = setting.FaviconUrl;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Settings(SettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var setting = _context.Settings.FirstOrDefault();
            bool isNew = setting == null;

            if (isNew)
            {
                setting = new Setting();
            }

            setting.SiteTitle = model.SiteTitle;
            setting.MetaDescription = model.MetaDescription;
            setting.FooterText = model.FooterText;

            if (model.LogoFile != null)
            {
                setting.LogoUrl = SaveImage(model.LogoFile, setting.LogoUrl);
            }

            if (model.FaviconFile != null)
            {
                setting.FaviconUrl = SaveImage(model.FaviconFile, setting.FaviconUrl);
            }

            if (isNew)
            {
                _context.Settings.Add(setting);
            }

            _context.SaveChanges();
            return RedirectToAction("Settings");
        }

        private string SaveImage(IFormFile file, string? oldUrl)
        {
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".svg" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Sadece png, jpg, jpeg veya svg dosyası yükleyebilirsiniz.");
                return oldUrl ?? "";
            }

            if (!string.IsNullOrEmpty(oldUrl))
            {
                var oldPath = Path.Combine(_env.WebRootPath, oldUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var folder = Path.Combine(_env.WebRootPath, "settings");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/settings/" + fileName;
        }
    }
}