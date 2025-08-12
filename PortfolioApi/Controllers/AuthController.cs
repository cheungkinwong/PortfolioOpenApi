using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var normalizedUsername = request.Username.ToUpperInvariant();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUsername);

            if (user == null)
            {
                return NotFound($"User with normalized username '{normalizedUsername}' not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized($"Invalid username or password - password check failed. {result}");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return Ok(new
            {
                Message = "Login successful",
                Username = user.UserName,
                Roles = roles,
                Token = token,
            });
        }

        [HttpPost("generate-hash")]
        public async Task<IActionResult> GeneratePasswordHash([FromBody] GenerateHashRequest request)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var dummyUser = new ApplicationUser(); // Just for the hasher
            var hash = hasher.HashPassword(dummyUser, request.Password);

            return Ok(new { Hash = hash });
        }

        public class GenerateHashRequest
        {
            public string Password { get; set; } = default!;
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var expiresInStr = jwtSettings["ExpiresInMinutes"];
            var expiresIn = string.IsNullOrEmpty(expiresInStr) ? 60 : Convert.ToDouble(expiresInStr);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }



}
