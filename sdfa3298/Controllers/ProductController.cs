using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sdfa3298.Models;
using sdfa3298.Repositories.Categories;
using sdfa3298.Repositories.Products;
using sdfa3298.ViewModels;

namespace sdfa3298.Controllers
{ 
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IWebHostEnvironment environment, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _environment = environment;
            _categoryRepository = categoryRepository;
        }

        private string? SaveImage(IFormFile image)
        {
            try
            {
                var types = image.ContentType.Split('/');

                if (types.Length != 2 || types[0] != "image")
                {
                    return null;
                }

                string imageName = $"{Guid.NewGuid().ToString()}.{types[1]}";
                string rootPath = _environment.WebRootPath;
                string imagePath = Path.Combine(rootPath, "images", imageName);

                using (var fileStream = System.IO.File.Create(imagePath))
                {
                    using (var imageStream = image.OpenReadStream())
                    {
                        imageStream.CopyTo(fileStream);
                    }
                }

                return imageName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.Products.Include(p => p.Category);

            return View(products);
        }

        public IActionResult Create()
        {
            var categories = _categoryRepository.Categories.ToList();
            var viewModel = new CreateProductVM
            {
                Categories = categories
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var res = _productRepository.IsExist(viewModel.Name!);
            if (res)
            {
                ModelState.AddModelError("Name", $"Продукт \"{viewModel.Name}\" вже існує");
                return View(viewModel);
            }
            string? imageName = null;
            if (viewModel.Image != null)
            {
                imageName = SaveImage(viewModel.Image);
                if (imageName == null)
                {
                    ModelState.AddModelError("Image", "Помилка при збереженні зображення");
                    return View(viewModel);
                }
            }
            var model = new Product
            {
                Name = viewModel.Name!,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Image = imageName,
                Amount = viewModel.Amount,
                CategoryId = viewModel.CategoryId
            };
            _productRepository.Create(model);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var model = _productRepository.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            var viewModel = new UpdateProductVM
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Amount = model.Amount,
                CategoryId = model.CategoryId,
                ExistingImage = model.Image,
                Categories = _categoryRepository.Categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var existingProduct = _productRepository.GetById(viewModel.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            if (existingProduct.Name != viewModel.Name)
            {
                var res = _productRepository.IsExist(viewModel.Name!);
                if (res)
                {
                    ModelState.AddModelError("Name", $"Продукт \"{viewModel.Name}\" вже існує");
                    return View(viewModel);
                }
            }
            string? imageName = existingProduct.Image;
            if (viewModel.Image != null)
            {
                imageName = SaveImage(viewModel.Image);
                if (imageName == null)
                {
                    ModelState.AddModelError("Image", "Помилка при збереженні зображення");
                    return View(viewModel);
                }
            }
            existingProduct.Name = viewModel.Name!;
            existingProduct.Description = viewModel.Description;
            existingProduct.Price = viewModel.Price;
            existingProduct.Image = imageName;
            existingProduct.Amount = viewModel.Amount;
            existingProduct.CategoryId = viewModel.CategoryId;
            _productRepository.Update(existingProduct);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var model = _productRepository.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            _productRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
