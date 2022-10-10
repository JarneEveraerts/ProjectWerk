using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllphiDB.Models
{
    internal class Visit
    {
        public int VisitID { get; set; }
        public Visitor Visitor { get; set; }
        public string Business { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Visit(Visitor visitor, string business, Employee employee, DateTime startDate)
        {
            Visitor = visitor;
            Business = business;
            Employee = employee;
            StartDate = startDate;
        }

        public Visit(Visitor visitor, string business, Employee employee, DateTime startDate, DateTime endDate)
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
    }
}