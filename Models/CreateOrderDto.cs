using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Models
{
    public class CreateOrderDto
    {
        public Guid CustomerId {get;set;}
        public ICollection<CreateOrderItemDto> OrderItems {get;set;} = new List<CreateOrderItemDto>();
    }
}