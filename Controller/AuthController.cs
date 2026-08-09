using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using MyFirstProject.Services;
using Microsoft.AspNetCore.Authorization;
namespace MyFirstProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
           var user = await authService.RegisterAsync(request);
           if (user is null)
              return BadRequest("Username is already exists");
            return Ok(user);
           
        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> login(UserDto request)
        {
           var result = await authService.LoginAsync(request);
           if (result is null)
              return BadRequest("Invalid username or password");
            return Ok(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("admin")]
        public IActionResult AuthenticatedEndPoint()
        {
            return Ok("You are admin");
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync(request);
            if (result is null || result.AccessToken is null || request.refreshToken is null)
                return Unauthorized("Invalid refresh token");
            return Ok(result);
        }
    }
}