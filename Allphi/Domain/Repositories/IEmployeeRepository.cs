using Domain.Models;

namespace Domain.Repositories;

public interface IEmployeeRepository
{
    #region GET

    Task<List<Employee>>? GetEmployees();

    Task<List<Employee>>? GetEmployeesByBusiness(string business);

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