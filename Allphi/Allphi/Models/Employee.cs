namespace Allphi.Models
{
    public class Employee : Entity
    {
        public Employee(string name, string firstname, string function, Business business, string? email, string? plate)
        {
            Name = name;
            FirstName = firstname;
            Function = function;
            Business = business;
            Email = email;
            Plate = plate;
        }

        public Employee()
        {
        }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string? Email { get; set; }
        public string Function { get; set; }
        public string? Plate { get; set; }
        public Business Business { get; set; }
    }
}