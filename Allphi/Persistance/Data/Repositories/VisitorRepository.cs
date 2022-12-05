using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Visitor>> GetVisitors()
        {
            List<Visitor> visitors = await _allphiContext.Visitor.Where(v => v.IsDeleted == false).ToListAsync();
            return visitors;
        }

        public async Task<List<Visitor>> GetVisitorsByBusiness(string Business)
        {
            List<Visitor> visitors = await _allphiContext.Visitor.Where(v => v.Business == Business && v.IsDeleted == false).ToListAsync();
            return visitors;
        }

        public Visitor GetVisitorByName(string name)
        {
            Visitor visitor = _allphiContext.Visitor.FirstOrDefault(v => v.Name == name && v.IsDeleted == false);
            return visitor;
        }

        public Visitor GetVisitorById(int id)
        {
            Visitor visitor = _allphiContext.Visitor.FirstOrDefault(v => v.Id == id && v.IsDeleted == false);
            return visitor;
        }

        public Visitor GetVisitorByMail(string email)
        {
            Visitor visitor = _allphiContext.Visitor.FirstOrDefault(v => v.Email == email && v.IsDeleted == false);
            return visitor;
        }

        #endregion GET

        #region CREATE

        public int CreateVisitor(Visitor visitor)
        {
            _allphiContext.Visitor.Add(visitor);
            _allphiContext.SaveChanges();

            return _allphiContext.Visitor.First(v => v.Id == visitor.Id).Id;
        }

        #endregion CREATE

        #region UPDATE

        public void UpdateVisitor(Visitor visitor)
        {
            _allphiContext.Visitor.Update(visitor);
            _allphiContext.SaveChanges();
        }

        #endregion UPDATE
    }
}