namespace Domain.Models.Dto_s;

public class ContractDTO : EntityDTO
{
    public BusinessDTO Business { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalSpaces { get; set; }
    public bool IsDeleted { get; set; }
}