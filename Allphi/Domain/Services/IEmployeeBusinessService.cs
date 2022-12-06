using Domain.Models;

namespace Domain.Services
{
    public interface IEmployeeBusinessService
    {
        Business GetBusinessByIdForEmployee(int id);
    }
}