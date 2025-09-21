﻿using sdfa3298.Models;

namespace sdfa3298.ViewModels
{
    public class CreateProductVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}
