﻿using Domain.Models;
using Domain.Services;

namespace Persistance.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AllphiContext _allphiContext;

    public EmployeeRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    #region GET

    public List<Employee> GetEmployees()
    {
        List<Employee> employees = _allphiContext.Employee.ToList();
        return employees;
    }

    public List<Employee> GetEmployeesByBusiness(Business business)
    {
        List<Employee> employees = _allphiContext.Employee.Where(E => E.Business == business).ToList();
        return employees;
    }

    public List<Employee> GetEmployeesByName(string name)
    {
        List<Employee> employees = _allphiContext.Employee.Where(e => e.Name.Contains(name) || e.FirstName.Contains(name)).ToList();
        return employees;
    }

    public Employee GetEmployeeById(int id)
    {
        Employee employee = _allphiContext.Employee.First(e => e.Id == id);
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
    }

    #endregion UPDATE

    #region DELETE

    public void DeleteEmployee(Employee employee)
    {
        _allphiContext.Employee.Remove(employee);
    }

    #endregion DELETE
}