using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using DeliverySystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MyFirstProject.Models.Entities;

namespace DeliverySystem.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "admin")]
        public IQueryable<Delivery> GetDeliveries()
        {
            return deliveryService.GetDeliveries();
        }
        [HttpGet]
        [Route("get-delivery/{id:guid}")]
        public async Task<ActionResult<Delivery>> GetDeliveryById(Guid id)
        {
            var Delivery = await deliveryService.GetDeliveryByIdAsync(id);
            if (Delivery is null)
                return BadRequest("Delivery Not Found");
            return Ok(Delivery);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("add-delivery")]
        public async Task<ActionResult<Delivery>> AddDelivery(CreateDeliveryDto dto)
        {
            var Delivery = await deliveryService.CreateDeliveryAsync(dto);
            if (Delivery is null)
                return BadRequest("Invalid Delivery");
            return Ok(Delivery);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("assign-driver")]
        public async Task<ActionResult<Delivery>> AssignDriver(Guid deliveryId,Guid driverId)
        {
            var Delivery = await deliveryService.AssignDriverAsync(deliveryId,driverId);
            if (Delivery is null)
                return BadRequest("Invalid Delivery");
            return Ok(Delivery);
        }
        [Authorize(Roles = "driver")]
        [HttpGet]
        [Route("arrive-delivery/{id:guid}")]
        public async Task<ActionResult<Delivery>> MarkAsArrived(Guid id)
        {
            var Delivery = await deliveryService.MarkAsArrivedAsync(id);
            if (Delivery is null)
                return BadRequest("Invalid Delivery");
            return Ok(Delivery);
        }
        [Authorize(Roles = "driver")]
        [HttpGet]
        [Route("start-delivery/{id:guid}")]
        public async Task<ActionResult<Delivery>> StartDelivery(Guid id)
        {
            var Delivery = await deliveryService.StartDeliveryAsync(id);
            if (Delivery is null)
                return BadRequest("Invalid Delivery");
            return Ok(Delivery);
        }
        [Authorize(Roles = "driver")]
        [HttpGet]
        [Route("complete-delivery/{id:guid}")]
        public async Task<ActionResult<Delivery>> CompleteDelivery(Guid id)
        {
            var Delivery = await deliveryService.CompleteDeliveryAsync(id);
            if (Delivery is null)
                return BadRequest("Invalid Delivery");
            return Ok(Delivery);
        }

    }
}