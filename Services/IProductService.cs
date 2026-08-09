using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Services
{
    public interface IProductService
    {
        IQueryable<Product> GetProducts();

        Task<Product?> GetProductByIdAsync(Guid id);

        Task<Product> CreateProductAsync(CreateProductDto dto);

        Task<Product?> UpdateProductAsync(
            Guid id,
            CreateProductDto dto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}