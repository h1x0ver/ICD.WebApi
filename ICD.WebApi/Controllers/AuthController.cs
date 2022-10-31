using ICD.Business.DTO_s.Auth;
using ICD.Entity.Identity;
using ICD.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ICD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        public AuthController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDTO)
        {
            AppUser appUser = new AppUser();
            appUser.Email = registerDTO.Email;
#pragma warning disable CS8601 // Possible null reference assignment.
            appUser.Lastname = registerDTO.Lastname;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
            appUser.Firstname = registerDTO.Firstname;
#pragma warning restore CS8601 // Possible null reference assignment.
            appUser.UserName = registerDTO.Username;
            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                string error = "";
                foreach (var item in result.Errors)
                {
                    error += item.Description + "\n";
                }
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response(4010, error));
            }
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null) return StatusCode(StatusCodes.Status405MethodNotAllowed, new Response(4005, "Username does not exist idiot"));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return StatusCode(StatusCodes.Status401Unauthorized, "Invalid Password retard...");
            var issuer = _config.GetSection("JWT:issuer").Value;
            var audience = _config.GetSection("JWT:audience").Value;
            var secretKey = _config.GetSection("JWT:secretKey").Value;
            List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Firstname", user.Firstname),
            new Claim("Id", user.Id),
            new Claim("Lastname",  user.Lastname)
        };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(n => new Claim(ClaimTypes.Role, n)));
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //jwt security token
            JwtSecurityToken securityToken = new(
                audience: audience,
                signingCredentials: signingCredentials,
                issuer: issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4).AddMonths(1)
            );
            //jwt sec. tokeni stringe cevirirem burda:
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return Ok(token);
        }
    }
}
