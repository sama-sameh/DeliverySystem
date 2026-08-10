using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Models.Entities;

namespace DeliverySystem.Services
{
    public class DriverService : IDriverService
    {
        private readonly ApplicationDbContext context;

        public DriverService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Driver> GetDrivers()
        {
            return context.Drivers
                .Include(d => d.User);
        }

        // Get driver by ID
        public async Task<Driver?> GetDriverByIdAsync(Guid id)
        {
            return await context.Drivers
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.DriverId == id);
        }

        // Create driver
        public async Task<Driver> CreateDriverAsync(
            CreateDriverDto dto)
        {
            // Check User exists
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == dto.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException(
                    "User not found.");
            }

            // Check if user is already a driver
            var driverExists = await context.Drivers
                .AnyAsync(d => d.UserId == dto.UserId);

            if (driverExists)
            {
                throw new InvalidOperationException(
                    "This user is already a driver.");
            }

            var driver = new Driver
            {
                DriverId = Guid.NewGuid(),
                UserId = dto.UserId,
                DriverStatus = "Available"
            };

            context.Drivers.Add(driver);

            await context.SaveChangesAsync();

            return driver;
        }

        public async Task<Driver?> UpdateDriverAsync(
            Guid id,
            UpdateDriverDto dto)
        {
            var driver = await context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return null;

            var validStatuses = new[]
            {
            "Available",
            "Busy",
            "OnDelivery",
            "Offline",
            "Suspended"
        };

            if (!validStatuses.Contains(dto.DriverStatus))
            {
                throw new ArgumentException(
                    "Invalid driver status.");
            }

            driver.DriverStatus = dto.DriverStatus;

            await context.SaveChangesAsync();

            return driver;
        }

        public async Task<bool> DeleteDriverAsync(Guid id)
        {
            var driver = await context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return false;

            // Don't delete driver with existing deliveries
            var hasDeliveries = await context.Deliveries
                .AnyAsync(d => d.DriverId == id);

            if (hasDeliveries)
            {
                throw new InvalidOperationException(
                    "Cannot delete a driver with existing deliveries.");
            }

            context.Drivers.Remove(driver);

            await context.SaveChangesAsync();

            return true;
        }

        // Change driver status
        public async Task<Driver?> ChangeDriverStatusAsync(
            Guid id,
            string status)
        {
            var driver = await context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return null;

            var validStatuses = new[]
            {
            "Available",
            "Assigned",
            "OnDelivery",
            "Offline",
            "Suspended"
        };

            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException(
                    "Invalid driver status.");
            }
            driver.DriverStatus = status;

            await context.SaveChangesAsync();

            return driver;
        }
        public IQueryable<Delivery> GetDriverDeliveries(
            Guid driverId)
        {
            return context.Deliveries
                .Where(d => d.DriverId == driverId)
                .Include(d => d.order)
                    .ThenInclude(o => o.customer);
        }
    }
}