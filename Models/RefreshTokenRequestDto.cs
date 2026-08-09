using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyFirstProject.Models
{
    public class RefreshTokenRequestDto
    {
        public Guid UserId {get;set;}
        public required string refreshToken {get;set;}
    }
}