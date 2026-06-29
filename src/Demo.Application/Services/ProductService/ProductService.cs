using Demo.Application.Interfaces;
using Demo.Application.Requests.Products;
using Demo.Application.Results;
using Demo.Application.Results.ProductResults;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Services.ProductService
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ResultsGeneric<Product> CheckResult(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return ResultsGeneric<Product>.Fail("Ma san pham khong duoc de trong");
            }
            Product? product = _productRepository.FindByCode(code);
            if (product == null) 
            {
                return ResultsGeneric<Product>.Fail("khong tim thay san pham");
            }
            return ResultsGeneric<Product>.Success(product,"tim thay san pham");
        }
        public List<Product> GetAlllProducts() 
        {
            return _productRepository.GetAll();
        }
        public ResultsGeneric<Product> AddProduct(AddProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return ResultsGeneric<Product>.Fail("Ma san pham khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return ResultsGeneric<Product>.Fail("Ten san pham khong duoc de trong.");
            }

            if (request.Price <= 0)
            {
                return ResultsGeneric<Product>.Fail("Gia san pham phai lon hon 0.");
            }

            Product? existingProduct = _productRepository.FindByCode(request.Code);

            if (existingProduct != null)
            {
                return ResultsGeneric<Product>.Fail("Ma san pham da ton tai.");
            }

            Product product = new()
            {
                Code = request.Code,
                Name = request.Name,
                Price = request.Price
            };

            _productRepository.Add(product);

            return ResultsGeneric<Product>.Success(product,"Add Thanh Cong");
        }
        public ResultsGeneric<Product> UpdateProduct(UpdateProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return ResultsGeneric<Product>.Fail("Ma san pham khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return ResultsGeneric<Product>.Fail("Ten san pham khong duoc de trong.");
            }

            if (request.Price <= 0)
            {
                return ResultsGeneric<Product>.Fail("Gia san pham phai lon hon 0.");
            }

            Product? product = _productRepository.FindByCode(request.Code);

            if (product == null)
            {
                return ResultsGeneric<Product>.Fail("Khong tim thay san pham.");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            _productRepository.Update(product);

            return ResultsGeneric<Product>.Success(product, "Update Thanh Cong");
        }
        public ResultsGeneric<Product> DeleteProduct(DeleteProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return ResultsGeneric<Product>.Fail("Ma san pham khong duoc de trong.");
            }

            Product? product = _productRepository.FindByCode(request.Code);

            if (product == null)
            {
                return ResultsGeneric<Product>.Fail("Khong tim thay san pham.");
            }

            _productRepository.Delete(request.Code);

            return ResultsGeneric<Product>.Success(product, "Xoa Thanh cong");
        }
    }
}
