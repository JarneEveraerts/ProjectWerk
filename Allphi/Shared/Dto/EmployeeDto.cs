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

    public EmployeeDto()
    {
    }
}