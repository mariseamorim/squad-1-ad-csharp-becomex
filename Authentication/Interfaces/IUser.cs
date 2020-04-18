namespace Authentication.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Token { get; set; }
    }
}