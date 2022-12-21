namespace Domain.Models.Dto_s;

public class VisitDTO : EntityDTO
{
    public VisitorDTO Visitor { get; set; }
    public ContractDTO Business { get; set; }
    public EmployeeDTO Employee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDeleted { get; set; }
}