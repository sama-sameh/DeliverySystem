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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> CancelOrderAsync(Guid id)
        {
           var order = await ChangeOrderStatusAsync(id,"Cancelled");
           return order is not null;
        }

        public  async Task<Order?> ChangeOrderStatusAsync(Guid id, string status)
        {
            var order = await context.Orders
               .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
                return null;
            order.OrderStatus = status;
            await context.SaveChangesAsync();
            return order;
        }
        private async Task<Customer?> CheckCustomer(Guid customerId)
        {
            return await context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        private async Task<List<Product>> CheckProducts(List<Guid> productIds)
        {
            return await context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            var customer = await CheckCustomer(dto.CustomerId);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            var productIds = dto.OrderItems
                .Select(i => i.ProductId)
                .ToList();

            var products = await CheckProducts(productIds);

            if (products.Count != productIds.Count)
                throw new KeyNotFoundException(
                    "One or more products were not found.");

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                PaymentStatus = "Pending",
                OrderStatus = "Pending",
                TotalAmount = 0
            };

            decimal totalAmount = 0;

            foreach (var item in dto.OrderItems)
            {
                var product = products
                    .First(p => p.ProductId == item.ProductId);

                var totalPrice = product.Price * item.Quantity;

                var orderItem = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = totalPrice
                };

                order.OrderItems.Add(orderItem);

                totalAmount += totalPrice;
            }

            order.TotalAmount = totalAmount;

            context.Orders.Add(order);

            await context.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await context.Orders
               .Include(o => o.customer)
               .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.product)
            .Include(o => o.delivery)
            .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public IQueryable<Order> GetOrders()
        {
            return context.Orders
            .Include(o => o.customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.product);
        }
    }
}