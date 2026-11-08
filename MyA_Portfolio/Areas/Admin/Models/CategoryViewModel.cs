using MyA_Portfolio.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyA_Portfolio.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Katagori adı zorunludur")]
        [StringLength(30, ErrorMessage = "Kategori adı en fazla 30 karakter olabilir.")]
        public string? CategoryName { get; set; }
       
    }
}
