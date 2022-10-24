namespace Domain.Services;

public interface IUserRepository
{
    public bool Login(string username, string password);
}