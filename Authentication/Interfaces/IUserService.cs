using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Interfaces
{
    public interface IUserService
    {
        IUser Authenticate(string email, string password, params string[] parameters);
    }
}
