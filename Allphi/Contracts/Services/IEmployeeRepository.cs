using Contracts.DTO;
namespace Contracts.Services;

public interface IEmployeeRepository
{
    #region GET

    List<Employee>? GetEmployees();

    List<Employee>? GetEmployeesByBusiness(Business business);

    List<Employee>? GetEmployeesByName(string name);
    Employee GetEmployeeByName(string name);

    Employee? GetEmployeeById(int id);

    Employee? GetEmployeeByPlate(string licensePlate);

    #endregion GET

    #region CREATE

    void CreateEmployee(Employee employee);

    #endregion CREATE

    #region UPDATE

    void UpdateEmployee(Employee employee);

    #endregion UPDATE
}