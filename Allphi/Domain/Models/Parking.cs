namespace Domain.Models
{
    public class Parking : Entity
    {
        public Parking(Employee? employee, Visitor? visitor, string? plate)
        {
            Employee = employee;
            Visitor = visitor;
            Plate = plate;
        }

        public Parking(string? plate)
        {
            Plate = plate;
        }

        public Parking()
        {
        }

        public Visitor? Visitor { get; set; }
        public Employee? Employee { get; set; }
        public string? Plate { get; set; }
    }
}