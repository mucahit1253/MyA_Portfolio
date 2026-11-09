using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyA_Portfolio.Areas.Admin.Models
{
    public class SettingViewModel
    {
        public int SettingId { get; set; }

        [Required(ErrorMessage = "Site başlığı zorunludur.")]
        [StringLength(100, ErrorMessage = "Site başlığı en fazla 100 karakter olabilir.")]
        public string? SiteTitle { get; set; }

        [StringLength(300, ErrorMessage = "Meta açıklama en fazla 300 karakter olabilir.")]
        public string? MetaDescription { get; set; }

        [StringLength(200, ErrorMessage = "Footer yazısı en fazla 200 karakter olabilir.")]
        public string? FooterText { get; set; }

        public string? LogoUrl { get; set; }
        public IFormFile? LogoFile { get; set; }

        public string? FaviconUrl { get; set; }
        public IFormFile? FaviconFile { get; set; }
    }
}