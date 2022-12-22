using Domain.Models;

namespace Shared.Dto;

public class ParkingSpotDto : EntityDto
{
    public VisitorDto? Visitor { get; set; }
    public EmployeeDto? Employee { get; set; }
    public string? Plate { get; set; }
    public BusinessDto? Reserved { get; set; }
    public bool IsDeleted { get; set; }

    public ParkingSpotDto(ParkingSpot parkingSpot)
    {
        Id = parkingSpot.Id;
        Visitor = parkingSpot.Visitor != null ? new VisitorDto(parkingSpot.Visitor) : null;
        Employee = parkingSpot.Employee != null ? new EmployeeDto(parkingSpot.Employee) : null;
        Plate = parkingSpot.Plate;
        Reserved = parkingSpot.Reserved != null ? new BusinessDto(parkingSpot.Reserved) : null;
        IsDeleted = parkingSpot.IsDeleted;
    }

    public ParkingSpotDto()
    {
    }
}