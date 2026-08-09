using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Services
{
    public interface ICustomerService
    {
        IQueryable <Customer> GetCustomers();
        Task<Customer?> CreateCustomerAsync(CreateCustomerDto dto);
        Task<Customer?> UpdateCustomerAsync(Guid id,UpdateCustomerDto dto);
        IQueryable<Order> GetCustomerOrders(Guid customerId);

    }
}