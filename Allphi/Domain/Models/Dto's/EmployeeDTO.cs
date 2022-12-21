namespace Domain.Models.Dto_s;

public class EmployeeDTO : EntityDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsDeleted { get; set; }
}