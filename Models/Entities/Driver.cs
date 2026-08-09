using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class Driver
    {
        public Guid DriverId { get; set; }
        public Guid  UserId { get; set; }
        public User User{get;set;} = null!;
        public string DriverStatus{get;set;}= string.Empty;
        public ICollection<Delivery> Deliveries {get;set;} = new List<Delivery>();
        
    }
}