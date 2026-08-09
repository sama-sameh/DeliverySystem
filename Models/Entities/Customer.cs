using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class Customer
    {
        public Guid CustomerId{get;set;}
        public Guid  UserId { get; set; }
        public string Address{get;set;} = string.Empty;
        public string PhoneNumber{get;set;} = string.Empty;
        public User User{get;set;} = null!;
        public ICollection<Order> Orders {get;set;} = new List<Order>();

    }
}