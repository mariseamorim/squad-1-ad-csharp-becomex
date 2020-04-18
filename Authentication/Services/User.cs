using System;
using Authentication.Interfaces;

namespace Authentication
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
