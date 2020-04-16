using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace CentralDeErrosApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]//permite que qualquer usuario acesse o token
        [HttpPost]
        public IActionResult RequestToken([FromBody]Users requestUser)
        {

            if (requestUser.Email == requestUser.Email && requestUser.Password == requestUser.Password)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name , requestUser.Name),

                };
                //recebe uma instancia da classe SymmetricSecurityKey 
                //armazenando a chave de criptografia usada na criação do token
                var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["Secret"]));

                //recebe um objeto do tipo SigninCredentials contendo a chave de 
                //criptografia e o algoritmo de segurança empregados na geração 
                // de assinaturas digitais para tokens
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                var token = new JwtSecurityToken(
                    issuer: "macoratti.net",
                    audience: "macoratti.net",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)

                }); ;

            }

            return BadRequest("Credenciais Inválidas");

        }

    }
}
