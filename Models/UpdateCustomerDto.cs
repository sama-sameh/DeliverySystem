using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models
{
    public class UpdateCustomerDto
    {
        public string Email {get;set;} = string.Empty;
        public string Address{get;set;} = string.Empty;
        public string PhoneNumber{get;set;} = string.Empty;
    }
}