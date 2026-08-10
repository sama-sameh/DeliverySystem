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
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "admin")]
        public IQueryable<Driver> GetDriverss()
        {
            return driverService.GetDrivers();
        }
        [HttpGet]
        [Route("get-driver/{id:guid}")]
        public async Task<ActionResult<Driver>> GetDriverById(Guid id)
        {
            var driver = await driverService.GetDriverByIdAsync(id);
            if (driver is null)
                return BadRequest("Driver Not Found");
            return Ok(driver);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("add-driver")]
        public async Task<ActionResult<Driver>> AddDriver(CreateDriverDto dto)
        {
            var driver = await driverService.CreateDriverAsync(dto);
            if (driver is null)
                return BadRequest("Invalid Driver");
            return Ok(driver);
        }
        [Authorize(Roles = "admin,driver")]
        [HttpPost]
        [Route("update-driver")]
        public async Task<ActionResult<Driver>> UpdateDriverAsync(Guid id,UpdateDriverDto dto)
        {
            var driver = await driverService.UpdateDriverAsync(id,dto);
            if (driver is null)
                return BadRequest("Invalid Driver");
            return Ok(driver);
        }
        

    }
}