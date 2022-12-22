using Newtonsoft.Json;

namespace Domain.Models
{
    public class Contract : Entity
    {
        public Contract(Business business, DateTime startDate, DateTime endDate, int totalSpaces)
        {
            Business = business;
            StartDate = startDate;
            EndDate = endDate;
            TotalSpaces = totalSpaces;
            IsDeleted = false;
        }

        public Contract()
        {
        }

        [JsonProperty]
        public int Id { get; set; }

        public Business Business { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalSpaces { get; set; }
        public bool IsDeleted { get; set; }
    }
}