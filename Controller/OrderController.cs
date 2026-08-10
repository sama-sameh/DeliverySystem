using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;
using MyFirstProject.Services;

namespace DeliverySystem.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "admin")]
        public IQueryable<Order> GetProducts()
        {
            return orderService.GetOrders();
        }
        [HttpGet]
        [Route("get-order/{id:guid}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order is null)
                return BadRequest("Order Not Found");
            return Ok(order);
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(CreateOrderDto dto)
        {
            var order = await orderService.CreateOrderAsync(dto);
            if (order is null)
                return BadRequest("Invalid Product");
            return Ok(order);
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("id:guid")]
        public async Task<ActionResult<bool>> CancelOrder(Guid id)
        {
            var isCancelled = await orderService.CancelOrderAsync(id);
            if (!isCancelled)
                return BadRequest("Invalid Product");
            return Ok(isCancelled);
        }

    }
}