using AutoMapper;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{email}/{password}")]
        public IActionResult Login(string email, string password) {

            User u = _userRepository.checkLogin(email, password);
            if(u == null)
            {
                return Unauthorized();
            }
            string role = _userRepository.GetRoleByEmail(email);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };
            var token = CreateToken(authClaims);
            string newToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(newToken);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
