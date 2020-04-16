using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    public interface IUser
    {
        bool RegisterUser(string email, string password, string name);

    }
}
