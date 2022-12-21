namespace Domain.Models
{
    public class ParkingSpot : Entity
    {
        public ParkingSpot(Employee? employee, Visitor? visitor, string? plate, Business? reserved)
        {
            Employee = employee;
            Visitor = visitor;
            Plate = plate;
            Reserved = reserved;
            IsDeleted = false;
        }

        public ParkingSpot(Business? reserved)
        {
            Employee = null;
            Visitor = null;
            Plate = null;
            Reserved = reserved;
            IsDeleted = false;
        }

        public ParkingSpot()
        {
        }

        public Visitor? Visitor { get; set; }
        public Employee? Employee { get; set; }
        public string? Plate { get; set; }
        public Business? Reserved { get; set; }
        public bool IsDeleted { get; set; }
    }
}