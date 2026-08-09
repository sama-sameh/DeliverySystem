using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Services
{
    public interface IOrderService
    {
        IQueryable<Order> GetOrders();
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<Order> CreateOrderAsync(CreateOrderDto dto);
       Task<bool> CancelOrderAsync(Guid id);
       Task<Order?> ChangeOrderStatusAsync(
        Guid id,
        string status);
    }
}