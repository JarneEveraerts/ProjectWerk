using Domain.Models;
using Domain.Repositories;

namespace AllphiTests.MockClasses
{
    internal class MockVisitorRepo : IVisitorRepository
    {
        List<Visitor> VisitorList = new List<Visitor>();
        public void CreateVisitor(Visitor visitor)
        {
            VisitorList.Add(visitor);
        }

        public Visitor GetVisitorById(int id)
        {
            return VisitorList[id];
        }

        public Visitor GetVisitorByMail(string mail)
        {
            return VisitorList.Find(visitor => visitor.Email == mail);
        }

        public Visitor GetVisitorByName(string name)
        {
            return VisitorList.Find(visitor => visitor.Name == name);
        }

        public List<Visitor> GetVisitors()
        {
            return VisitorList;
        }

        public List<Visitor> GetVisitorsByBusiness(string Business)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisitor(Visitor visitor)
        {
            VisitorList[visitor.Id] = visitor;
        }

        int IVisitorRepository.CreateVisitor(Visitor visitor)
        {
            VisitorList.Add(visitor);
            return VisitorList.Count;
        }
    }
}