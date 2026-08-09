// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Query;
// using Microsoft.AspNetCore.OData.Routing.Controllers;
// using MyFirstProject.Data;
// using MyFirstProject.Models.Entities;

// namespace MyFirstProject.Controller
// {
//     public class EmployeeOdataController : ODataController
//     {
//         private readonly ApplicationDbContext context;

//         public EmployeeOdataController(ApplicationDbContext context)
//         {
//             this.context = context;
//         } 
//         [EnableQuery]
//         [HttpGet]
//         //IQueryable -> the query still isn't executed
//         public IQueryable <Employee> Get()
//         {
//             //return Query
//             return context.Employees;
//         }

//     }
// }