using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CentralDeErrosApi.Data;
using CentralDeErrosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CentralDeErros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
    
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public UsersController( ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        /// <summary>
        /// Listar os usuarios
        /// </summary>
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// Criar um usuario
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            
        }

        /// Criar um usuario e gerar token
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] Users requestUser)
        {
            
            if (requestUser.Email == requestUser.Email && requestUser.Password == requestUser.Password)
            {
                return BuildToken(requestUser);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken BuildToken(Users user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };

            var Key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var credenciais = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);


            // Tempo de expiração do token
            var expires = DateTime.Now.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "acelera dev",
                audience: "acelera dev",
                claims: claims,
                signingCredentials: credenciais);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires

            };
        }

    }
}
