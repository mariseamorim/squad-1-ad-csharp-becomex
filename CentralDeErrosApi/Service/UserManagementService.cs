using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using CentralDeErrosApi.Models.DTOs;
using CentralDeErrosApi.Models.ViewModels;
using CentralDeErrosApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CentralDeErrosApi.Service
{
    public class UserManagementService : IUser
    {
        private readonly string _secret;
        private readonly string _username;
        private readonly string _password;
        private readonly string _emissor;
        private readonly string _audiencia;
        private readonly int _expiracao_Horas;
        private ApplicationContext _context;
        private IConfiguration _configuration;

        public UserManagementService(IConfiguration configuration, ApplicationContext context)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettingsDTO = appSettingsSection.Get<AppSettingsDTO>();

            var authenticationSection = configuration.GetSection("Authentication");
            var authenticationDTO = authenticationSection.Get<AuthenticationDTO>();

            //DecryptFromBase64
            _secret = authenticationDTO.Secret;
            _username = authenticationDTO.UserName;
            _password = authenticationDTO.Password;
            _emissor = appSettingsDTO.Emissor;
            _audiencia = appSettingsDTO.Audiencia;
            _expiracao_Horas = appSettingsDTO.ExpiracaoHoras;
            _context = context;
            _configuration = configuration;
        }

        public bool ValidateUserLogin(string email, string password)
        {
            if (_context.Users.Any(x => x.Email == email && x.Password == password))
                return true;
            else
                return false;
        }

        public bool ValidateTokenExpiration(string email, string password)
        {
            var user = _context.Users.First(x => x.Email == email && x.Password == password);
            if (DateTime.Now >= user.Expiration)
                return true;
            else
                return false;
        }

        public bool ValidateUserExistById(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }

        public bool ValidateUserExistByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool ValidateUserIdAdmin(string email, string password)
        {
            var emailDecoded = Encrypt.FromBase64String(_username);
            var passDecoded = Encrypt.FromBase64String(_password);
            return email == emailDecoded && password == passDecoded ? true : false;
        }


        public async Task<ActionResult<Users>> Create(RegisterUserViewModel registroUsuario)
        {
            var user = new Users
            {
                Name = registroUsuario.Name,
                Password = registroUsuario.Password,
                Email = registroUsuario.Email,
                Token = RecoverJWT(registroUsuario.Email),
                Expiration = DateTime.Now.AddHours(_expiracao_Horas)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Password = "";
            return user;
        }

        public async Task<string> GenerateJWT(UserViewModel user)
        {
            if (ValidateUserLogin(user.Email, user.Password) || ValidateUserIdAdmin(user.Email, user.Password))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _emissor,
                    audience: _audiencia,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_expiracao_Horas),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
                return "Credenciais Invalidas";
        }

        public string RecoverJWT(string email)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, email) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _emissor,
                audience: _audiencia,
                claims: claims,
                expires: DateTime.Now.AddHours(_expiracao_Horas),
                signingCredentials: creds);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int GetExpirationHours()
        {
            return _expiracao_Horas;
        }
    }
}
