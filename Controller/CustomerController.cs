using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;
using MyFirstProject.Services;

namespace MyFirstProject.Controller
{
    
    [ApiController]
    [Route("api/[controller]")]
    // public class CustomerController: ODataController
    public class CustomerController:ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(CreateCustomerDto request)
        {
           var user = await customerService.CreateCustomerAsync(request);
           if (user is null)
              return BadRequest("Username is already exists");
            return Ok(user);
        } 
    }
}