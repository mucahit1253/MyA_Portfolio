using MyA_Portfolio.Entities;
using System.ComponentModel.DataAnnotations;

public class SkillViewModel
{
    public int SkillId { get; set; }

    [Required(ErrorMessage = "Yetenek adı zorunludur.")]
    [StringLength(50, ErrorMessage = "Yetenek adı en fazla 50 karakter olabilir.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
    public int? SkillGroupId { get; set; }

    public SkillGroup? SkillGroup { get; set; }
}
