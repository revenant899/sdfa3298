using sdfa3298.Models;

namespace sdfa3298.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
        public PaginationVM Pagination { get; set; } = new();
        public string? Category { get; set; } = null;
    }
}
