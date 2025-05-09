using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DTO;
using OnlineStore.Models;
using BCrypt.Net;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DbOnlineStore _context;

        public AccountController(DbOnlineStore context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                return BadRequest(new { message = "User already exists." });

           

            var user = new User
            {
                ID = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                DateCreate = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful", userId = user.ID });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
                return Unauthorized(new { message = "Invalid credentials." });

    //        var claims = new List<Claim>
    //{
    //    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
    //    new Claim(ClaimTypes.Name, user.Name),
    //    new Claim(ClaimTypes.Email, user.Email),
    //    new Claim(ClaimTypes.Role, user.Role.Name)
    //};

    //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    //        var principal = new ClaimsPrincipal(identity);

    //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Return user data in response
            return Ok(new { id = user.ID, username = user.Name, email = user.Email });
        }

        //[HttpGet("me")]
        //public IActionResult Me()
        //{
        //    if (User.Identity?.IsAuthenticated == true)
        //    {
        //        return Ok(new
        //        {
        //            Username = User.Identity.Name,
        //            Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
        //        });
        //    }
        //    return Unauthorized();
        //}

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Ok();
        //}

      
    }
}