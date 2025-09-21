using Microsoft.AspNetCore.Mvc;
using sdfa3298.Models;
using sdfa3298.Services;
using sdfa3298.ViewModels;
using System.Diagnostics;
using sdfa3298.Repositories.Products;
using Microsoft.Extensions.Options;






namespace sdfa3298.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly PaginationVM _pagination;
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IProductRepository productRepository, IOptions<PaginationVM> pagination)
        {
            _pagination = pagination.Value;
            _logger = logger;
            _context = context;
            _productRepository = productRepository;
        }

        public IActionResult AddToCart(int productId)
        {
            if (productId == 0)
            {
                return NotFound();
            }

            HttpContext.Session.AddToCart(productId);

            return RedirectToAction("Index");
        }

        public IActionResult Index(string? category, int page = 1)
        {
            _pagination.Page = page;

            var products = !string.IsNullOrEmpty(category)
                ? _productRepository.GetByCategory(category, _pagination)
                : _productRepository.GetByPagination(_pagination);

            var viewModel = new HomeVM
            {
                Products = products,
                Categories = _context.Categories,
                Pagination = _pagination,
                Category = category
            };
            return View(viewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
