using System.Linq;
using System.Threading.Tasks;
using CentralDeErrosApi.Service;
using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace CentralDeErros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly UserManagementService _userManagementService;
        public LoginController(IConfiguration configuration, ApplicationContext context)
        {
            _userManagementService = new UserManagementService(configuration, context);
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Register(RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            if (_userManagementService.ValidateUserExistByEmail(viewModel.Email))
                return BadRequest("Email já cadastrado");

            var result = await _userManagementService.Create(viewModel);
            
            viewModel.Password = "";
            viewModel.PasswordConfirm = "";

            return Ok(result);
        }

        [HttpPost("Entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            if (!_userManagementService.ValidateUserExistByEmail(viewModel.Email))
                return BadRequest("Email informado não cadastrado.");
            else if (!_userManagementService.ValidateUserLogin(viewModel.Email, viewModel.Password))
                return BadRequest("Senha informada incorreta.");
            else if (_userManagementService.ValidateTokenExpiration(viewModel.Email, viewModel.Password))
                return BadRequest("O Token do seu usuário expirou. Gere um novo Token e atualize o seu usuário.");
            else
                return Ok("Usuário logado com sucesso.");
        }
    }
}
