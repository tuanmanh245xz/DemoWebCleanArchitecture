
using Demo.Application.Interfaces;
using Demo.Application.Requests;
using Demo.Application.Results;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ProductCheckResult CheckResult(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return ProductCheckResult.Fail("Ma san pham khong duoc de trong");
            }
            Product? product = _productRepository.FindByCode(code);
            if (product == null) 
            {
                return ProductCheckResult.Fail("khong tim thay san pham");
            }
            return ProductCheckResult.Success(product);
        }
        public List<Product> GetAlllProducts() 
        {
            return _productRepository.GetAll();
        }
        public AddProductResult AddProduct(AddProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return AddProductResult.Fail("Ma san pham khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return AddProductResult.Fail("Ten san pham khong duoc de trong.");
            }

            if (request.Price <= 0)
            {
                return AddProductResult.Fail("Gia san pham phai lon hon 0.");
            }

            Product? existingProduct = _productRepository.FindByCode(request.Code);

            if (existingProduct != null)
            {
                return AddProductResult.Fail("Ma san pham da ton tai.");
            }

            Product product = new()
            {
                Code = request.Code,
                Name = request.Name,
                Price = request.Price
            };

            _productRepository.Add(product);

            return AddProductResult.Success();
        }
        public UpdateProductResult UpdateProduct(UpdateProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return UpdateProductResult.Fail("Ma san pham khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return UpdateProductResult.Fail("Ten san pham khong duoc de trong.");
            }

            if (request.Price <= 0)
            {
                return UpdateProductResult.Fail("Gia san pham phai lon hon 0.");
            }

            Product? product = _productRepository.FindByCode(request.Code);

            if (product == null)
            {
                return UpdateProductResult.Fail("Khong tim thay san pham.");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            _productRepository.Update(product);

            return UpdateProductResult.Success();
        }
        public DeleteProductResult DeleteProduct(DeleteProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return DeleteProductResult.Fail("Ma san pham khong duoc de trong.");
            }

            Product? product = _productRepository.FindByCode(request.Code);

            if (product == null)
            {
                return DeleteProductResult.Fail("Khong tim thay san pham.");
            }

            _productRepository.Delete(request.Code);

            return DeleteProductResult.Success();
        }
    }
}
