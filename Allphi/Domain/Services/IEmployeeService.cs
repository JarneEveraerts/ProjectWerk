using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IEmployeeService
    {
        void CreateEmployee(CreateAndUpdateEmployeeDTO createAndUpdateEmployeeDTO);
        void DeleteEmployee(int id);
        int GetBusinessIdByEmployeeName(string name);
        Employee GetEmployeeByName(string name);
        Employee GetEmployeeByPlate(string licenseplate);
        int GetEmployeeIdByName(string name);
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> GetEmployeesByBusiness(string businessName);
        void UpdateEmployee(CreateAndUpdateEmployeeDTO createAndUpdateEmployeeDTO, int id);
    }
}