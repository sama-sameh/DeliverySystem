using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class Product
    {
        public Guid ProductId {get;set;}
        public string Sku {get;set;} = string.Empty;
        public string Name{get;set;} = string.Empty;
        public decimal Price {get;set;} 
        public ICollection<OrderItem?> OrderItems { get; set; }= new List<OrderItem>();
    }
}