using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Requests
{
    public class UpdateProductRequest
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
