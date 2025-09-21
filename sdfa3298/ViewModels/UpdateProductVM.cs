using System.ComponentModel.DataAnnotations;
using sdfa3298.Models;

namespace sdfa3298.ViewModels
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Вкажіть назву товару")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина 100 символів")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути більшою за 0")]
        public double Price { get; set; }
        public IFormFile? Image { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Вкажіть категорію")]
        public int CategoryId { get; set; }
        public string? ExistingImage { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
