using Microsoft.AspNetCore.Mvc;

namespace sdfa3298.ViewModels
{
    public class PaginationVM
    {
        public int Page { get; set; } = 1;
        public int PageCount { get; set; } = 1;
        public int TotalCount { get; set; } = 0;
        public int PageSize { get; set; } = 12;
    }
}
