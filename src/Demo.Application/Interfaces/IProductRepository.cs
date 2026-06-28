
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Interfaces
{
    public interface IProductRepository
    {
        Product? FindByCode(string code);
        List<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Delete(string code);
    }
}
