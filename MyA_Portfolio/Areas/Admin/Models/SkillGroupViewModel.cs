using System.ComponentModel.DataAnnotations;

namespace MyA_Portfolio.Areas.Admin.Models
{
    public class SkillGroupViewModel
    {
        public int SkillGroupId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(50, ErrorMessage = "Başlık en fazla 50 karakter olabilir.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(300, ErrorMessage = "Açıklama en fazla 300 karakter olabilir.")]
        public string? Description { get; set; }

        [StringLength(50, ErrorMessage = "İkon sınıfı en fazla 50 karakter olabilir.")]
        public string? IconClass { get; set; }
    }
}
