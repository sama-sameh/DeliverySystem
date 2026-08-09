using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class Delivery
    {
        public Guid DeliveryId{ get; set; }
        public Guid OrderId{ get; set; }
        public Guid DriverId{ get; set; }
        public string Status{ get; set; } = string.Empty;
        public DateTime? StartedAt{ get; set; }
        public DateTime? ArrivedAt{ get; set; }
        public DateTime AssignedAt{ get; set; }
        public Order order{get;set;} = null!;
        public Driver driver{get;set;} = null!;

    }
}