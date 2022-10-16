namespace Domain.Models
{
    public class ParkingSpot : Entity
    {
        public ParkingSpot(Employee? employee, Visitor? visitor, string? plate, string? reserved)
        {
            Employee = employee;
            Visitor = visitor;
            Plate = plate;
            Reserved = reserved;
        }

        public ParkingSpot()
        {
        }

        public Visitor? Visitor { get; set; }
        public Employee? Employee { get; set; }
        public string? Plate { get; set; }
        public string? Reserved { get; set; }
    }
}