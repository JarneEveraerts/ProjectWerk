using System;

namespace Allphi.Models
{
    public class Visit : Entity
    {
        public Visit(Visitor visitor, Business business, Employee employee, DateTime startDate, DateTime? endDate)
        {
            Visitor = visitor;
            Business = business;
            Employee = employee;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Visit()
        {
        }

        public Visitor Visitor { get; set; }
        public Business Business { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}