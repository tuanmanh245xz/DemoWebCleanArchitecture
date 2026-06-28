using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Results
{
    public class DeleteProductResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";

        public static DeleteProductResult Success()
        {
            return new DeleteProductResult
            {
                IsSuccess = true,
                Message = "Xoa san pham thanh cong."
            };
        }

        public static DeleteProductResult Fail(string message)
        {
            return new DeleteProductResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
