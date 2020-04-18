using Authentication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Authentication.Services
{
    public class UserService
    {
        private const string DefaultNamingContext = "defaultNamingContext";
        private const string SamAccountName = "sAMAccountName";
        private const string GivenName = "givenName";
        private const string Surname = "sn";

        private readonly string _secret;
        private readonly string _username;
        private readonly string _password;
        private readonly string _domainhost;
        private readonly string _domainname;

        public UserService(IConfiguration configuration)
        {
            _secret = configuration.GetSection("Authentication").GetSection("Secret").Value;
            _username = configuration.GetSection("Authentication").GetSection("Username").Value;
            _password = configuration.GetSection("Authentication").GetSection("Password").Value;
        }

        /// <summary>
        /// Valida e retorna o usuário que está se autenticando
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IUser Authenticate(string email, string password, params string[] parameters)
        {
            try
            {
                Guid clientId = Guid.Empty;
                if (parameters.Any())
                    clientId = new Guid(parameters.FirstOrDefault());

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, email.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, email.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var user = new User
                {
                    Email = email,
                    Token = $"Bearer {tokenHandler.WriteToken(token)}"
                };
                return user;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }

        private IUser SetUserToken(IUser user)
        {
            var secretKey = Encoding.ASCII.GetBytes(_secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.GivenName, user.Email ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
