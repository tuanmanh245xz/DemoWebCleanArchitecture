using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Results.ProductResults
{
    public class AddProductResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";

        public static AddProductResult Success()
        {
            return new AddProductResult
            {
                IsSuccess = true,
                Message = "Them san pham thanh cong."
            };
        }

        public static AddProductResult Fail(string message)
        {
            return new AddProductResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
