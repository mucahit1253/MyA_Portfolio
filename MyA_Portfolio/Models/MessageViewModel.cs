using System.ComponentModel.DataAnnotations;

namespace MyA_Portfolio.Models
{
    public class MessageViewModel
    {
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100)]
        public string? NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(150)]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000)]
        public string? MessageDetail { get; set; }
    }
}
