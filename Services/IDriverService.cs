using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using MyFirstProject.Models.Entities;

namespace DeliverySystem.Services
{
    public interface IDriverService
    {
        IQueryable<Driver> GetDrivers();

        // Business operations
        Task<Driver?> GetDriverByIdAsync(Guid id);

        Task<Driver> CreateDriverAsync(CreateDriverDto dto);

        Task<Driver?> UpdateDriverAsync(
            Guid id,
            UpdateDriverDto dto);

        Task<bool> DeleteDriverAsync(Guid id);

        Task<Driver?> ChangeDriverStatusAsync(
            Guid id,
            string status);

        IQueryable<Delivery> GetDriverDeliveries(Guid driverId);
    }
}