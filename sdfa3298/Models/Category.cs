﻿namespace sdfa3298.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; } = [];
    }
}
