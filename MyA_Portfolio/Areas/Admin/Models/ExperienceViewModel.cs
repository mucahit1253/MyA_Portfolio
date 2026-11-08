using System.ComponentModel.DataAnnotations;

namespace MyA_Portfolio.Areas.Admin.Models
{
    public class ExperienceViewModel
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Şirket adı en fazla 100 karakter olabilir.")]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Alt başlık en fazla 100 karakter olabilir.")]
        public string? SideTitle { get; set; }

        [Required(ErrorMessage = "Çalışma zamanı zorunludur.")]
        [StringLength(50)]
        public string? Duration { get; set; }

        [Required(ErrorMessage = "Çalışma türü zorunludur.")]
        public string? WorkType { get; set; }

        public bool IsActive { get; set; }

        public List<int> SelectedSkillIds { get; set; } = new();
    }
}
