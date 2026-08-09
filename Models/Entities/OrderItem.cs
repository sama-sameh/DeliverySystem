using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId{get;set;}
        public Guid OrderId{get;set;}
        public Guid ProductId{get;set;}
        public int Quantity{get;set;}
        public decimal UnitPrice{get;set;}
        public decimal TotalPrice{get;set;}
        public Order order {get;set;} = null!;
        public Product product {get;set;} =null!;

    }
}