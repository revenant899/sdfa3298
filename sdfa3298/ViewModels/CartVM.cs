using Microsoft.AspNetCore.Mvc;
using sdfa3298.Models;

namespace sdfa3298.ViewModels
{
    public class CartVM
    {
        private int count = 1;
        public required Product Product { get; set; }
        public int Count { get; set; } = 1;
        public double TotalPrice => Product.Price * Count;
    }
}
