using API_Exercise.Data;
using API_Exercise.DTO;
using API_Exercise.Helpers;
using API_Exercise.Models;
using API_Exercise.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Exercise.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _jwtService;

        public AuthController(AppDbContext context, IJwtTokenService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO model)
        {
            // 1️⃣ Check password match
            if (model.Password != model.ConfirmPassword)
                return BadRequest("Passwords do not match");

            // 2️⃣ Check if email already exists
            var emailExists = await _context.Users
                .AnyAsync(x => x.Email == model.Email);

            if (emailExists)
                return Conflict("Email already registered");

            // 3️⃣ Create new user
            var user = new User
            {
                Email = model.Email,
                PasswordHash = PasswordHelper.HashPassword(model.Password),
                IsActive = true
            };

            // 4️⃣ Save to DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 5️⃣ (OPTIONAL) Auto-login after register
            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                message = "User registered successfully",
                token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.IsActive);

            if (user == null)
                return Unauthorized("Invalid email or password");

            if (!PasswordHelper.VerifyPassword(model.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password");

            var token = _jwtService.GenerateToken(user);

            return Ok(token);
        }
    }
}
