using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class Order
    {
        public Guid OrderId {get;set;}
        public Guid CustomerId {get;set;}
        public DateTime OrderDate {get;set;}
        public decimal TotalAmount {get;set;}
        public string PaymentStatus {get;set;}= string.Empty;
        public string OrderStatus {get;set;} = string.Empty;
        public Customer customer {get;set;} = null!;
        public ICollection<OrderItem> OrderItems {get;set;} = new List<OrderItem>();
        public Delivery? delivery{get;set;}
    }
}