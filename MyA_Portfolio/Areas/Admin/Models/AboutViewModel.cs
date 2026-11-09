using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyA_Portfolio.Areas.Admin.Models
{
    public class AboutViewModel
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100)]
        public string? NameSurname { get; set; }

        [Required(ErrorMessage = "Ünvan zorunludur.")]
        [StringLength(100)]
        public string? JobTitle { get; set; }

        [Required(ErrorMessage = "Lokasyon zorunludur.")]
        [StringLength(100)]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Uzmanlık zorunludur.")]
        [StringLength(200)]
        public string? Expert { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "İş durumu zorunludur.")]
        public string? OpenToWork { get; set; }

        public string? CvFileUrl { get; set; }

        public IFormFile? CvFile { get; set; }
    }
}
