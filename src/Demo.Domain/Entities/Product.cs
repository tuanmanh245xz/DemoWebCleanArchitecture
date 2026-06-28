using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain.Entities
{
    public class Product
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
