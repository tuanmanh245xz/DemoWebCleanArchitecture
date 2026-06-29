using Demo.Application.Requests.Products;
using Demo.Application.Results;
using Demo.Application.Results.ProductResults;
using Demo.Application.Services.ProductService;
using Demo.Domain.Entities;
using Demo.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            List<Product> products = _productService.GetAlllProducts();

            List<ProductListItemViewModel> model = products.Select(product =>
                new ProductListItemViewModel
                {
                    Code = product.Code,
                    Name = product.Name,
                    Price = product.Price
                }).ToList();

            return View(model);
        }

        public IActionResult Details(string code)
        {
            ResultsGeneric<Product> result = _productService.CheckResult(code);

            if (!result.IsSuccessed || result.Data == null)
            {
                TempData["ErrorMessage"] = result.Messsage;
                return RedirectToAction("Index");
            }

            ProductListItemViewModel model = new()
            {
                Code = result.Data.Code,
                Name = result.Data.Name,
                Price = result.Data.Price
            };

            return View(model);
        }

        public IActionResult Add()
        {
            return View(new AddProductViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel model)
        {
            AddProductRequest request = new()
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price
            };

            ResultsGeneric<Product> result = _productService.AddProduct(request);

            if (!result.IsSuccessed)
            {
                ViewBag.ErrorMessage = result.Messsage;
                return View(model);
            }

            TempData["SuccessMessage"] = result.Messsage;
            return RedirectToAction("Index");
        }

        public IActionResult Update(string code)
        {
            ResultsGeneric<Product> result = _productService.CheckResult(code);

            if (!result.IsSuccessed || result.Data == null)
            {
                TempData["ErrorMessage"] = result.Messsage;
                return RedirectToAction("Index");
            }

            UpdateProductViewModel model = new()
            {
                Code = result.Data.Code,
                Name = result.Data.Name,
                Price = result.Data.Price
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateProductViewModel model)
        {
            UpdateProductRequest request = new()
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price
            };

            ResultsGeneric<Product> result = _productService.UpdateProduct(request);

            if (!result.IsSuccessed)
            {
                ViewBag.ErrorMessage = result.Messsage;
                return View(model);
            }

            TempData["SuccessMessage"] = result.Messsage;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string code)
        {
            ResultsGeneric<Product> result = _productService.CheckResult(code);

            if (!result.IsSuccessed || result.Data == null)
            {
                TempData["ErrorMessage"] = result.Messsage;
                return RedirectToAction("Index");
            }

            DeleteProductViewModel model = new()
            {
                Code = result.Data.Code,
                Name = result.Data.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(DeleteProductViewModel model)
        {
            DeleteProductRequest request = new()
            {
                Code = model.Code
            };

            ResultsGeneric<Product> result = _productService.DeleteProduct(request);

            if (!result.IsSuccessed)
            {
                ViewBag.ErrorMessage = result.Messsage;
                return View(model);
            }

            TempData["SuccessMessage"] = result.Messsage;
            return RedirectToAction("Index");
        }
    }
}
