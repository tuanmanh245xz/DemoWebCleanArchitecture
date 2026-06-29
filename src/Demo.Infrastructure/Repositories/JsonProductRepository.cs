using Demo.Application.Interfaces;
using Demo.Domain.Entities;
using System.Text.Json;

namespace Demo.Infrastructure.Repositories
{
    public class JsonProductRepository : IProductRepository
    {
        private readonly string _filePath = "products.json";
        public void Add(Product product)
        {
            List<Product> products = ReadProducts();
            products.Add(product);
            SaveProducts(products);
        }

        public void Delete(string code)
        {
            List<Product> products = ReadProducts();

            Product? product = products.FirstOrDefault(item =>
                item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                return;
            }

            products.Remove(product);

            SaveProducts(products);
        }

       

        public Product? FindByCode(string code)
        {
            List<Product> products = ReadProducts();

            Product? product = products.FirstOrDefault(item =>
                item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (product == null) 
            {
                return null;
            }
           return product;
        }

        public List<Product> GetAll()
        {
           return ReadProducts();
        }

        public void Update(Product product)
        {
            List<Product> products = ReadProducts();
            Product? OldProduct = products.FirstOrDefault(item =>
               item.Code.Equals(product.Code, StringComparison.OrdinalIgnoreCase));

            if (OldProduct == null) 
            {
                return;
            }
            OldProduct.Name = product.Name;
            OldProduct.Price = product.Price;
            SaveProducts(products);

        }
        private List<Product> ReadProducts()
        {
            if (!File.Exists(_filePath))
            {
                return CreateDefaultProducts();
            }

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Product>();
            }

            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(json);

            return products ?? new List<Product>();
        }

        private void SaveProducts(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
        private List<Product> CreateDefaultProducts()
        {
            List<Product> products = new()
        {
            new Product { Code = "P001", Name = "Laptop", Price = 1200 },
            new Product { Code = "P002", Name = "Mouse", Price = 25 },
            new Product { Code = "P003", Name = "Keyboard", Price = 50 }
        };

            SaveProducts(products);

            return products;
        }
    }
}
