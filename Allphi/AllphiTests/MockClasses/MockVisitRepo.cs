using Domain.Models;

namespace AllphiTests.MockClasses
{
    public class MockVisitRepo : IVisitRepository
    {
        List<Visit> Visitlist = new List<Visit>();
        public void CreateVisit(Visit visit)
        {
            Visitlist.Add(visit);
        }

        public Visit GetVisitByVisitor(Visitor visitor)
        {
            return Visitlist.Find(visit => visit.Visitor == visitor);
        }

        public List<Visit> GetVisits()
        {
            return Visitlist;
        }

        public List<Visit> GetVisitsByBusiness(Business business)
        {
            return Visitlist.FindAll(visit => visit.Business == business);
        }

        public List<Visit> GetVisitsByEmployee(Employee employee)
        {
            return Visitlist.FindAll(visit => visit.Employee == employee);
        }

        public List<Visit> GetVisitsByVisitor(Visitor visitor)
        {
            return Visitlist.FindAll(visit => visit.Visitor == visitor);
        }

        public void UpdateVisit(Visit visit)
        {
            Visitlist[visit.Id] = visit;
        }
    }
}