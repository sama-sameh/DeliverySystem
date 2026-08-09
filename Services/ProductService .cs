using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Product> GetProducts()
        {
            return context.Products;
        }

        // Get product by ID
        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        // Create product
        public async Task<Product> CreateProductAsync(
            CreateProductDto dto)
        {
            // Check SKU uniqueness
            var skuExists = await context.Products
                .AnyAsync(p => p.Sku == dto.Sku);

            if (skuExists)
            {
                throw new InvalidOperationException(
                    "A product with this SKU already exists.");
            }

            // Validate price
            if (dto.Price < 0)
            {
                throw new ArgumentException(
                    "Product price cannot be negative.");
            }

            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Sku = dto.Sku,
                Name = dto.Name,
                Price = dto.Price
            };

            context.Products.Add(product);

            await context.SaveChangesAsync();

            return product;
        }

        // Update product
        public async Task<Product?> UpdateProductAsync(
            Guid id,
            CreateProductDto dto)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return null;

            // Check SKU uniqueness
            var skuExists = await context.Products
                .AnyAsync(p =>
                    p.Sku == dto.Sku &&
                    p.ProductId != id);

            if (skuExists)
            {
                throw new InvalidOperationException(
                    "A product with this SKU already exists.");
            }

            if (dto.Price < 0)
            {
                throw new ArgumentException(
                    "Product price cannot be negative.");
            }

            product.Sku = dto.Sku;
            product.Name = dto.Name;
            product.Price = dto.Price;

            await context.SaveChangesAsync();

            return product;
        }

        // Delete product
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return false;

            // Prevent deleting a product
            // that already exists in orders
            var usedInOrders = await context.OrderItems
                .AnyAsync(oi => oi.ProductId == id);

            if (usedInOrders)
            {
                throw new InvalidOperationException(
                    "Cannot delete a product that exists in an order.");
            }

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return true;
        }

    }
}