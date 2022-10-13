using Domain.Models;
using Domain.Services;
using Persistance.Data;
using System.Collections.Generic;
using System.Linq;

namespace Persistance.Data.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly AllphiContext allphiContext;

        public VisitorRepository()
        {
            allphiContext = new AllphiContext();
        }

        public Visitor GetVisitor(string email)
        {
            Visitor visitor = allphiContext.Visitor.First(v => v.Email == email);
            return visitor;
        }

        public List<Visitor> GetVisitors()
        {
            List<Visitor> visitors = allphiContext.Visitor.ToList();
            return visitors;
        }

        public void CreateVisitor(Visitor visitor)
        {
            allphiContext.Add(visitor);
        }
    }
}