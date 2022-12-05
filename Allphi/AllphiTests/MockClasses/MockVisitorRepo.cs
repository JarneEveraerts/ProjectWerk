using Domain.Models;
using Domain.Repositories;

namespace AllphiTests.MockClasses
{
    internal class MockVisitorRepo : IVisitorRepository
    {
        public void CreateVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public Visitor GetVisitorById(int id)
        {
            throw new NotImplementedException();
        }

        public Visitor GetVisitorByMail(string mail)
        {
            throw new NotImplementedException();
        }

        public Visitor GetVisitorByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Visitor> GetVisitors()
        {
            throw new NotImplementedException();
        }

        public List<Visitor> GetVisitorsByBusiness(string Business)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        int IVisitorRepository.CreateVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}