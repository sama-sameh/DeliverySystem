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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        [EnableQuery]
        public IQueryable<Product> GetProducts()
        {
            return productService.GetProducts();
        }
        [HttpGet]
        [Route("id:guid")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product is null)
                return BadRequest("Product Not Found");
            return Ok(product);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("add-product")]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto dto)
        {
            var product = await productService.CreateProductAsync(dto);
            if (product is null)
                return BadRequest("Invalid Product");
            return Ok(product);
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Guid id, CreateProductDto dto)
        {
            var product = await productService.UpdateProductAsync(id, dto);
            if (product is null)
                return BadRequest("Invalid Product");
            return Ok(product);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            bool isdeleted = await productService.DeleteProductAsync(id);
            if (!isdeleted)
                return BadRequest("The Product isn't found");
            return Ok(isdeleted);
        }

    }
}