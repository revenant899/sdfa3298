﻿
using System.ComponentModel.DataAnnotations;

namespace sdfa3298.ViewModels
{
    public class CreateCategoryVM
    {
        [Required(ErrorMessage = "Вкажіть назву категорії")]
        [MaxLength(50, ErrorMessage = "Максимальна довжина 50 символів")]
        public string? Name { get; set; }
    }
}
