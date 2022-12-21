namespace Domain.Models.Dto_s;

public class ParkingSpotDTO : EntityDTO
{
    public EmployeeDTO? Employee { get; set; }
    public VisitorDTO? Visitor { get; set; }
    public string? Plate { get; set; }
    public ContractDTO? Reserved { get; set; }
    public bool IsDeleted { get; set; }
}