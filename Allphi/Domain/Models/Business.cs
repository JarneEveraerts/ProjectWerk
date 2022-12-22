using Newtonsoft.Json;

namespace Domain.Models
{
    public class Business : Entity
    {
        public Business(string name, string btw, string email)
        {
            Name = name;
            Btw = btw;
            Email = email;
        }

        public Business(string name, string btw, string email, string? address, string? phone)
        {
            Name = name;
            Btw = btw;
            Email = email;
            Address = address;
            Phone = phone;
            IsDeleted = false;
        }

        [JsonProperty]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Btw { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsDeleted { get; set; }
    }
}