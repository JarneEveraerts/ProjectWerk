using Allphi.Models;
using Allphi.Services;
using System.Collections.Generic;
using System.Linq;

namespace Allphi.Data.Repositorys
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