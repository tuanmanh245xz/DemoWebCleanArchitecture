using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Results.ProductResults
{
    public class ProductCheckResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public Product? Product { get; set; }

        public static ProductCheckResult Success(Product product)
        {
            return new ProductCheckResult
            {
                IsSuccess = true,
                Message = "Tim thay san pham.",
                Product = product
            };
        }

        public static ProductCheckResult Fail(string message)
        {
            return new ProductCheckResult
            {
                IsSuccess = false,
                Message = message,
                Product = null
            };
        }
    }
}
