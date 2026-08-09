// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using MyFirstProject.Data;
// using MyFirstProject.Models;
// using MyFirstProject.Models.Entities;

// namespace MyFirstProject.Controller
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class EmployeesController : ControllerBase
//     {
//         private readonly ApplicationDbContext dbContext;
//         public EmployeesController(ApplicationDbContext dbContext)
//         {
//             this.dbContext = dbContext;
//         }
//         [HttpGet]
//         public IActionResult GetAllEmployees()
//         {
//             var AllEmployees = dbContext.Employees.ToList();
//             return Ok(AllEmployees);
            
//         }
//         [HttpPost]
//         public IActionResult AddEmployee(AddEmployeeDto employeeDto)
//         {
//             var employee = new Employee()
//             {
//                 Name = employeeDto.Name,
//                 Email = employeeDto.Email,
//                 Phone = employeeDto.Phone,
//                 Salary = employeeDto.Salary
//             };
//             dbContext.Employees.Add(employee);
//             dbContext.SaveChanges();
//             return Ok(employee);

//         }
//         [HttpGet]
//         [Route("id:guid")]
//          public IActionResult GetEmployeeById(Guid id)

//         {
//             var employee = dbContext.Employees.Find(id);
//             if (employee is null)
//             {
//                 return NotFound();
//             }
//             return Ok(employee);
//         }
//         [HttpPut]
//         [Route("id:guid")]
//         public IActionResult UpdateEmployee(Guid id,AddEmployeeDto employeeDto)
//         {
//             var emp = dbContext.Employees.Find(id);
//              if (emp is null)
//             {
//                 return NotFound();
//             }
//             emp.Name = employeeDto.Name;
//             emp.Email = employeeDto.Email;
//             emp.Phone = employeeDto.Phone;
//             emp.Salary = employeeDto.Salary;
//             dbContext.SaveChanges();
//             return Ok(emp);
//         }
//         [HttpDelete]
//         [Route("id:guid")]
//         public IActionResult DeleteEmployee(Guid id)
//         {
//              var emp = dbContext.Employees.Find(id);
//              if (emp is null)
//             {
//                 return NotFound();
//             }
//             dbContext.Employees.Remove(emp);
//             dbContext.SaveChanges();
//             return Ok();
//         }
//     }
// }