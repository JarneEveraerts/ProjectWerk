using Domain.Models;

namespace Shared.Dto;

public class VisitDto : EntityDto
{
    public VisitorDto Visitor { get; set; }
    public BusinessDto Business { get; set; }
    public EmployeeDto Employee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDeleted { get; set; }

    public VisitDto(Visit visit)
    {
        Id = visit.Id;
        Visitor = new VisitorDto(visit.Visitor);
        Business = new BusinessDto(visit.Business);
        Employee = new EmployeeDto(visit.Employee);
        StartDate = visit.StartDate;
        EndDate = visit.EndDate;
        IsDeleted = visit.IsDeleted;
    }

    public VisitDto(VisitorDto visitor, BusinessDto business, EmployeeDto employee, DateTime startDate, DateTime? endDate)
    {
        Visitor = visitor;
        Business = business;
        Employee = employee;
        StartDate = startDate;
        EndDate = endDate;
        IsDeleted = false;
    }

    public VisitDto()
    {
    }
}