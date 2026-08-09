using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MyFirstProject.Data;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyFirstProject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext context;
        private readonly IAuthService authService;

        public CustomerService(ApplicationDbContext context,IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }
        public async Task<Customer?> CreateCustomerAsync(CreateCustomerDto dto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
            var user = await authService.RegisterAsync(new UserDto
            {
                Username = dto.Username,
                Password = dto.Password,
                Role = dto.Role
            });
            if (user is null)
               return null;
            var customer = new Customer()
            {
                UserId = user.Id,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                User = user
            };
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return customer;
            }
            catch
           {
               await transaction.RollbackAsync();
               throw;
           }
        }
      
        public IQueryable<Order> GetCustomerOrders(Guid customerId)
        {
            return context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.product);
        }

        public IQueryable<Customer> GetCustomers()
        {
            return context.Customers
            .Include(c => c.User);
        }
        public async Task<Customer?> UpdateCustomerAsync(Guid id, UpdateCustomerDto dto)
        {
            var customer = await context.Customers
               .Include(c => c.User)
               .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
                return null;
            if (customer.User != null)
            {
                customer.User.Email = dto.Email;
            }
           customer.Address = dto.Address;
           customer.PhoneNumber = dto.PhoneNumber;
           await context.SaveChangesAsync();
           return customer;
        }
    }
}