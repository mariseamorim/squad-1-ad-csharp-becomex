using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Models;
using CentralDeErrosApi.Models.ViewModels;
using CentralDeErrosApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CentralDeErros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManagementService _userManagementService;

        public UsersController(IConfiguration configuration, ApplicationContext context)
        {
            _context = context;
            _userManagementService = new UserManagementService(configuration, context);
        }

        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        // GET: api/GetUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        /// <summary>
        /// Atualiza o token do usuário.
        /// </summary>
        // GET: api/AtualizeTokenUser
        [HttpPost("AtualizarToken")]
        public async Task<IActionResult> AtualizeTokenUser(UserViewModel user)
        {
            if (!_userManagementService.ValidateUserExistByEmail(user.Email))
                return BadRequest("Email informado não cadastrado.");
            else if (!_userManagementService.ValidateUserLogin(user.Email, user.Password))
                return BadRequest("Senha informada incorreta.");
            else
            {
                Users userFinded = _context.Users.First(e => e.Email == user.Email && e.Password == user.Password);
                userFinded.Token = _userManagementService.RecoverJWT(user.Email);
                userFinded.Expiration = DateTime.Now.AddHours(_userManagementService.GetExpirationHours());

                _context.Entry(userFinded).State = EntityState.Modified;
                _context.Users.Update(userFinded);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        /// <summary>
        /// Deleta um usuário.
        /// </summary>
        // GET: api/DeleteUser
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
