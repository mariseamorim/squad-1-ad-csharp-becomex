using CentralDeErrosApi.Data;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using System.Linq;

namespace CentralDeErrosApi.Service
{
    public class UserService : IUser
    {
        private ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            this._context = context;
        }

        public bool RegisterUser(string email, string password, string name)
        {
            _context.Users.Add(new Users { Email = email, Password = password, Name = name });

            if (_context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Name == name) != null)
            {
                return true;
            }

            return false;
        }
    }
}
