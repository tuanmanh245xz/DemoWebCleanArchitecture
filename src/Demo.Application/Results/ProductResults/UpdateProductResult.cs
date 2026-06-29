using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Results.ProductResults
{
    public class UpdateProductResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";

        public static UpdateProductResult Success()
        {
            return new UpdateProductResult
            {
                IsSuccess = true,
                Message = "Cap nhat san pham thanh cong."
            };
        }

        public static UpdateProductResult Fail(string message)
        {
            return new UpdateProductResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
