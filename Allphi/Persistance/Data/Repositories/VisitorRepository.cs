using Domain.Models;
using Domain.Services;

namespace Persistance.Data.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly AllphiContext _allphiContext;

        public VisitorRepository(AllphiContext allphiContext)
        {
            _allphiContext = allphiContext;
        }

        #region GET

        public List<Visitor> GetVisitors()
        {
            List<Visitor> visitors = _allphiContext.Visitor.ToList();
            return visitors;
        }

        public List<Visitor> GetVisitorsByBusiness(string Business)
        {
            List<Visitor> visitors = _allphiContext.Visitor.Where(v => v.Business == Business).ToList();
            return visitors;
        }

        public Visitor GetVisitorByName(string name)
        {
            Visitor visitor = _allphiContext.Visitor.First(v => v.Name == name);
            return visitor;
        }

        public Visitor GetVisitorById(int id)
        {
            Visitor visitor = _allphiContext.Visitor.First(v => v.Id == id);
            return visitor;
        }

        public Visitor GetVisitorByMail(string email)
        {
            Visitor visitor = _allphiContext.Visitor.First(v => v.Email == email);
            return visitor;
        }

        #endregion GET

        #region CREATE

        public void CreateVisitor(Visitor visitor)
        {
            _allphiContext.Visitor.Add(visitor);
            _allphiContext.SaveChanges();
        }

        #endregion CREATE

        #region UPDATE

        public void UpdateVisitor(Visitor visitor)
        {
            _allphiContext.Visitor.Update(visitor);
            _allphiContext.SaveChanges();
        }

        #endregion UPDATE

        #region DELETE

        public void DeleteVisitor(Visitor visitor)
        {
            _allphiContext.Visitor.Remove(visitor);
            _allphiContext.SaveChanges();
        }

        #endregion DELETE
    }
}