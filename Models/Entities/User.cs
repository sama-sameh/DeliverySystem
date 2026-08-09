using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities
{
    public class User
    {
        public Guid Id {get; set;}
        public string Username {get;set;} = string.Empty;
        public string Email {get;set;} = string.Empty;
        public string PasswordHash {get;set;} = string.Empty;
        public string Role {get;set;} = string.Empty;
        public string? RefreshToken {get;set;}
        public DateTime? RefreshTokenExpiryTime {get;set;}
        public Customer? customer{get;set;}
        public Driver? driver {get;set;}
    }
}