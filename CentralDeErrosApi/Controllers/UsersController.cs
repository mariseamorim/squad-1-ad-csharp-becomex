using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralDeErrosApi.DTO;
using CentralDeErrosApi.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErrosApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService _userManagementService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(UserService userManagementService,
                                SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManagementService = userManagementService;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

           // var result = await _userManagementService.Create(viewModel);

            //if (!result.Succeeded) return BadRequest(result.Errors);

            viewModel.Senha = "";
            viewModel.ConfirmaSenha = "";

            return Ok(viewModel);
        }

        [HttpPost("Entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Senha, false, true);

            if (!result.Succeeded)
            {
                return BadRequest("Usuário ou senha inválido.");
            }

            return Ok(viewModel);//(await _userManagementService.GerarJWT(viewModel.Email));
        }
    }
}
