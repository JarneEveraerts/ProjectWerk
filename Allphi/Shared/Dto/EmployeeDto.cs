using Domain.Models;

namespace Shared.Dto;

public class EmployeeDto : EntityDto
{
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string? Email { get; set; }
    public string Function { get; set; }
    public string? Plate { get; set; }
    public BusinessDto Business { get; set; }
    public bool IsDeleted { get; set; }

    public EmployeeDto(Employee employee)
    {
        Id = employee.Id;
        Name = employee.Name;
        FirstName = employee.FirstName;
        Email = employee.Email;
        Function = employee.Function;
        Plate = employee.Plate;
        Business = new BusinessDto(employee.Business);
        IsDeleted = employee.IsDeleted;
    }

    public EmployeeDto(string name, string firstName, string? email, string function, string? plate, BusinessDto business)
    {
        Name = name;
        FirstName = firstName;
        Email = email;
        Function = function;
        Plate = plate;
        Business = business;
        IsDeleted = false;
    }

    public EmployeeDto()
    {
    }
}