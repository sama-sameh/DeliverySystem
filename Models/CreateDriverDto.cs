using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySystem.Models
{
    public class CreateDriverDto
    {
         public Guid UserId { get; set; }

         public string DriverStatus { get; set; } = "Available";
    }
}