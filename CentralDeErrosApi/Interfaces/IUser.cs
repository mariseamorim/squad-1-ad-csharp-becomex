using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    interface IUser
    {
        bool RegisterUser(string email, string password, string name);

        bool Login(string email, string password);

        bool UserExists(int id);
    }
}
