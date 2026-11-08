using MyA_Portfolio.Entities;
using System.ComponentModel.DataAnnotations;

public class ProjectViewModel
{
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Proje başlığı zorunludur.")]
    [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
    public string? GithubUrl { get; set; }

    public string? CoverImageUrl { get; set; }
    public IFormFile? CoverImageFile { get; set; } // yüklenecek yeni dosya

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
    public int? CategoryId { get; set; }

    public Category? Category { get; set; }
}