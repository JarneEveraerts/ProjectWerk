using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AllphiContext _allphiContext;

    public EmployeeRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    #region GET

    public async Task<List<Employee>> GetEmployees()
    {
        List<Employee> employees = await _allphiContext.Employee.Where(e => e.IsDeleted == false).ToListAsync();
        return employees;
    }

    public async Task<List<Employee>> GetEmployeesByBusiness(string business)
    {
        List<Employee> employees = await _allphiContext.Employee.Where(e => e.Business.Name == business && e.IsDeleted == false).ToListAsync();
        return employees;
    }

    public List<Employee> GetEmployeesByName(string name)
    {
        List<Employee> employees = _allphiContext.Employee.Where(e => e.Name.Contains(name) || e.FirstName.Contains(name) && e.IsDeleted == false).ToList();
        return employees;
    }

    public Employee GetEmployeeByName(string name)
    {
        Employee employee = _allphiContext.Employee.FirstOrDefault(e => e.Name.Contains(name) || e.FirstName.Contains(name) && e.IsDeleted == false);
        return employee;
    }

    public Employee GetEmployeeById(int id)
    {
        Employee employee = _allphiContext.Employee.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);
        return employee;
    }

    public Employee GetEmployeeByPlate(string licensePlate)
    {
        Employee employee = _allphiContext.Employee.FirstOrDefault(e => e.Plate == licensePlate);
        return employee;
    }

    #endregion GET

    #region CREATE

    public void CreateEmployee(Employee employee)
    {
        _allphiContext.Employee.Add(employee);
        _allphiContext.SaveChanges();
    }

    #endregion CREATE

    #region UPDATE

    public void UpdateEmployee(Employee employee)
    {
        _allphiContext.Employee.Update(employee);
        _allphiContext.SaveChanges();
    }

    #endregion UPDATE
}