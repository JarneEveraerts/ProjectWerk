namespace Domain.Repositories;

public interface IUserRepository
{
    public bool Login(string username, string password);
}