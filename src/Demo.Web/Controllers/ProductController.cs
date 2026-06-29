using Demo.Application.Requests;
using Demo.Application.Results;
using Demo.Application.Services;
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
            ProductCheckResult result = _productService.CheckResult(code);

            if (!result.IsSuccess || result.Product == null)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            ProductListItemViewModel model = new()
            {
                Code = result.Product.Code,
                Name = result.Product.Name,
                Price = result.Product.Price
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

            AddProductResult result = _productService.AddProduct(request);

            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }

        public IActionResult Update(string code)
        {
            ProductCheckResult result = _productService.CheckResult(code);

            if (!result.IsSuccess || result.Product == null)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            UpdateProductViewModel model = new()
            {
                Code = result.Product.Code,
                Name = result.Product.Name,
                Price = result.Product.Price
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

            UpdateProductResult result = _productService.UpdateProduct(request);

            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string code)
        {
            ProductCheckResult result = _productService.CheckResult(code);

            if (!result.IsSuccess || result.Product == null)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            DeleteProductViewModel model = new()
            {
                Code = result.Product.Code,
                Name = result.Product.Name
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

            DeleteProductResult result = _productService.DeleteProduct(request);

            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}
