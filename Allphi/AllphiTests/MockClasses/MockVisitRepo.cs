using Domain.Models;
using Domain.Services;

namespace AllphiTests.MockClasses
{
    public class MockVisitRepo : IVisitRepository
    {
        public void CreateVisit(Visit visit)
        {
            throw new NotImplementedException();
        }

        public Visit GetVisitByVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetVisits()
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetVisitsByBusiness(Business business)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetVisitsByEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetVisitsByVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisit(Visit visit)
        {
            throw new NotImplementedException();
        }
    }
}