using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public bool Login(string email, string password)
        {
            _context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            if (_context.Users.FirstOrDefault(x => x.Email == email && x.Password == password) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }
    }
}
