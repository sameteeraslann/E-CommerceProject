using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProject.Web.Controllers
{
    public class ProductController:Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository,
                                 ICategoryRepository categoryRepository )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index() => View(await _productRepository.Get(x => x.Status != Entity.Enums.Status.Passive));

        
       public async Task<IActionResult> ProductByCategory(string categorySlug)
        {
            Category category = await _categoryRepository.FirstOrDefault(x => x.Slug == categorySlug);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;

            List<Product> products = await _productRepository.Get(x => x.CategoryId == category.Id);
            return View(products);
        }
    }
}
