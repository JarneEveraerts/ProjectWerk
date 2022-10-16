using Domain.Models;

namespace Domain.Services;

public interface IEmployeeRepository
{
    #region GET

    List<Employee> GetEmployees();

    List<Employee> GetEmployeesByBusiness(Business business);

    Employee GetEmployeeById(int id);

    Employee GetEmployeeByName(string name);

    #endregion GET

    #region CREATE

    void CreateEmployee(Employee employee);

    #endregion CREATE

    #region UPDATE

    void UpdateEmployee(Employee employee);

    #endregion UPDATE

    #region DELETE

    void DeleteEmployee(Employee employee);

    #endregion DELETE
}