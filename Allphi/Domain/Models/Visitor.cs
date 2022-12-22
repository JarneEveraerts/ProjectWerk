using Newtonsoft.Json;

namespace Domain.Models
{
    public class Visitor : Entity
    {
        public Visitor(string name, string email, string business, string? plate)
        {
            Name = name;
            Email = email;
            Business = business;
            Plate = plate;
            IsDeleted = false;
        }

        public Visitor()
        {
        }

        [JsonProperty]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Business { get; set; }
        public string? Plate { get; set; }
        public bool IsDeleted { get; set; }
    }
}