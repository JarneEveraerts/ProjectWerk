using Contracts.DTO;
namespace Contracts.Services;

public interface IUserRepository
{
    public bool Login(string username, string password);
}