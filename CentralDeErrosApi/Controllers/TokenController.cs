using System.Threading.Tasks;
using CentralDeErrosApi.Service;
using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace CentralDeErrosApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        public TokenController(IConfiguration configuration, ApplicationContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody]UserViewModel requestUser)
        {
            UserManagementService userManagementService = new UserManagementService(_configuration, _context);
            var tokenReturn = await userManagementService.GenerateJWT(requestUser);
            
            if (tokenReturn == "Credenciais Invalidas")
            {
                return BadRequest("Credenciais Inválidas");
            }
            else
                return Ok(new { tokenReturn });
        }
    }
}
