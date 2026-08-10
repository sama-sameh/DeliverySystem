using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using MyFirstProject.Models.Entities;

namespace DeliverySystem.Services
{
    public interface IDeliveryService
    {
        IQueryable<Delivery> GetDeliveries();
        Task<Delivery?> GetDeliveryByIdAsync(Guid id);

        Task<Delivery?> CreateDeliveryAsync(CreateDeliveryDto dto);

        Task<Delivery?> AssignDriverAsync(Guid deliveryId,Guid driverId);

        Task<Delivery?> StartDeliveryAsync(Guid deliveryId);

        Task<Delivery?> MarkAsArrivedAsync(Guid deliveryId);

        Task<Delivery?> CompleteDeliveryAsync(Guid deliveryId);
    }
}