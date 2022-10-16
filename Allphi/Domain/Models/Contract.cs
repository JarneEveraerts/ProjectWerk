using System;

namespace Domain.Models
{
    public class Contract : Entity
    {
        public Contract(Business business, DateOnly startDate, DateOnly endDate, int totalSpaces)
        {
            Business = business;
            StartDate = startDate;
            EndDate = endDate;
            TotalSpaces = totalSpaces;
        }

        public Contract()
        {
        }

        public Business Business { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalSpaces { get; set; }
    }
}