
namespace CentralDeErrosApi.Interfaces
{
    public interface IUser
    {
        bool ValidateUserLogin(string email, string password);
        bool ValidateUserExistById(int id);
        bool ValidateUserExistByEmail(string email);
    }
}
