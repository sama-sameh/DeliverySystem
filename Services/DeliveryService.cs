using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using MyFirstProject.Data;
using MyFirstProject.Models.Entities;
using MyFirstProject.Services;

namespace DeliverySystem.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ApplicationDbContext context;
        private readonly IOrderService orderService;
        private readonly IDriverService driverService;

        public DeliveryService(ApplicationDbContext context, IOrderService orderService,IDriverService driverService)
        {
            this.context = context;
            this.orderService = orderService;
            this.driverService = driverService;
        }
        
        public async Task<Delivery?> AssignDriverAsync(Guid deliveryId, Guid driverId)
        {
           var delivery =  await context.Deliveries.FindAsync(deliveryId);
           if (delivery is null)
              return null;
            var driver = context.Drivers
                 .FirstOrDefault(d => d.DriverId == driverId);
            if (driver is null || driver.DriverStatus != "Available")
               return null;    
            await driverService.ChangeDriverStatusAsync(driver.DriverId,"Assigned");
           delivery.DriverId = driverId;
           delivery.AssignedAt = DateTime.UtcNow;
           delivery.Status = "Assigned";
          
           await context.SaveChangesAsync();
           return delivery;

        }

        public async Task<Delivery?> CompleteDeliveryAsync(Guid deliveryId)
        {
            var delivery =  await context.Deliveries.FindAsync(deliveryId);
            if (delivery is null)
              return null;
            var order = await  orderService.GetOrderByIdAsync(delivery.OrderId);
            if (order is null)
              return null;
            await orderService.ChangeOrderStatusAsync(delivery.OrderId,"Delivered");
            await driverService.ChangeDriverStatusAsync(delivery.DriverId,"Available");
            delivery.Status = "Delivered";
            await context.SaveChangesAsync();
            return delivery;
        }

        public async Task<Delivery?> CreateDeliveryAsync(CreateDeliveryDto dto)
        {
            var order = await  orderService.GetOrderByIdAsync(dto.OrderId);
            if (order is null)
              return null;

            var delivery = new Delivery()
            {
                OrderId = dto.OrderId,
                Status = "Pending",
                order = order
            };
            await context.Deliveries.AddAsync(delivery);
            await context.SaveChangesAsync();
            return delivery;
        }

        public IQueryable<Delivery> GetDeliveries()
        {
            return context.Deliveries;
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(Guid id)
        {
            var delivery =  context.Deliveries.Find(id);
            if (delivery is null)
              return null;
            return delivery;
        }

        public async Task<Delivery?> MarkAsArrivedAsync(Guid deliveryId)
        {
            var delivery =  context.Deliveries.Find(deliveryId);
            if (delivery is null|| delivery.Status !="OutForDelivery")
              return null;
            delivery.Status = "Arrived";
            delivery.ArrivedAt = DateTime.UtcNow;
            context.SaveChanges();
            return delivery;
        }

        public async Task<Delivery?> StartDeliveryAsync(Guid deliveryId)
        {
            var delivery =  await context.Deliveries.FindAsync(deliveryId);
            if (delivery is null)
              return null;
            var driver = await driverService.GetDriverByIdAsync(delivery.DriverId);
            if (driver is null || driver.DriverStatus !="Assigned")
               return null;
            await driverService.ChangeDriverStatusAsync(driver.DriverId,"OnDelivery");
            delivery.Status = "OutForDelivery";
            delivery.StartedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return delivery;
        }
    }
}