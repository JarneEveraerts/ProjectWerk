using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AllphiContext _allphiContext;

    public EmployeeRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    private IQueryable<Employee> EmployeeStandard()
    {
        return _allphiContext.Employee.Include(e => e.Business);
    }

    #region GET

    public List<Employee> GetEmployees()
    {
        List<Employee> employees = EmployeeStandard().Where(e => e.IsDeleted == false).ToList();
        return employees;
    }

    public List<Employee> GetEmployeesByBusiness(Business business)
    {
        List<Employee> employees = EmployeeStandard().Where(e => e.Business == business && e.IsDeleted == false).ToList();
        return employees;
    }

    public List<Employee> GetEmployeesByName(string name)
    {
        List<Employee> employees = EmployeeStandard().Where(e => e.Name.Contains(name) || e.FirstName.Contains(name) && e.IsDeleted == false).ToList();
        return employees;
    }

    public Employee GetEmployeeByName(string name)
    {
        Employee employee = EmployeeStandard().FirstOrDefault(e => e.Name.Contains(name) || e.FirstName.Contains(name) && e.IsDeleted == false);
        return employee;
    }

    public Employee GetEmployeeById(int id)
    {
        Employee employee = EmployeeStandard().FirstOrDefault(e => e.Id == id && e.IsDeleted == false);
        return employee;
    }

    public Employee GetEmployeeByPlate(string licensePlate)
    {
        Employee employee = EmployeeStandard().FirstOrDefault(e => e.Plate == licensePlate);
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