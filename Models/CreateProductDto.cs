using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models
{
    public class CreateProductDto
    {
        public string Sku {get;set;} = string.Empty;
        public string Name{get;set;} = string.Empty;
        public decimal Price {get;set;} 
    }
}