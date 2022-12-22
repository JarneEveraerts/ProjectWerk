using Domain.Models;

namespace Shared.Dto;

public class VisitorDto : EntityDto
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Plate { get; set; }
    public string Business { get; set; }
    public bool IsDeleted { get; set; }

    public VisitorDto(Visitor visitor)
    {
        Id = visitor.Id;
        Name = visitor.Name;
        Email = visitor.Email;
        Plate = visitor.Plate;
        Business = visitor.Business;
        IsDeleted = visitor.IsDeleted;
    }

    public VisitorDto()
    {
    }
}