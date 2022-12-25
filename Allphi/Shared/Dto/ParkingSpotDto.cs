using Domain.Models;

namespace Shared.Dto;

public class ParkingSpotDto : EntityDto
{
    public VisitorDto? Visitor { get; set; }
    public EmployeeDto? Employee { get; set; }
    public string? Plate { get; set; }
    public int ReservedId { get; set; }
    public BusinessDto? Reserved { get; set; }
    public bool IsDeleted { get; set; }

    public ParkingSpotDto(ParkingSpot parkingSpot)
    {
        Id = parkingSpot.Id;
        Visitor = parkingSpot.Visitor != null ? new VisitorDto(parkingSpot.Visitor) : null;
        Employee = parkingSpot.Employee != null ? new EmployeeDto(parkingSpot.Employee) : null;
        Plate = parkingSpot.Plate;
        ReservedId = parkingSpot.ReservedId;
        Reserved = parkingSpot.Reserved != null ? new BusinessDto(parkingSpot.Reserved) : null;
        IsDeleted = parkingSpot.IsDeleted;
    }

    public ParkingSpotDto(VisitorDto visitor, EmployeeDto employee, string plate, BusinessDto reserved)
    {
        Visitor = visitor;
        Employee = employee;
        Plate = plate;
        ReservedId = reserved.Id;
        Reserved = reserved;
        IsDeleted = false;
    }

    public ParkingSpotDto()
    {
    }
}