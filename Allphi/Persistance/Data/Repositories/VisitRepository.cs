using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AllphiContext _allphiContext;

    public VisitRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    private IQueryable<Visit> VisitStandard()
    {
        return _allphiContext.Visit.Include(v => v.Visitor).Include(v => v.Business).Include(v => v.Employee);
    }

    #region GET

    public List<Visit> GetVisits()
    {
        List<Visit> visits = VisitStandard().Where(v => v.IsDeleted == false).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByEmployee(Employee employee)
    {
        List<Visit> visits = VisitStandard().Where(v => v.Employee == employee).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByBusiness(Business business)
    {
        List<Visit> visits = VisitStandard().Where(v => v.Business == business).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByVisitor(Visitor visitor)
    {
        List<Visit> visits = VisitStandard().Where(v => v.Visitor == visitor).ToList();
        return visits;
    }

    public Visit GetVisitByVisitor(Visitor visitor)
    {
        Visit visit = VisitStandard().FirstOrDefault(v => v.Visitor == visitor);
        return visit;
    }

    #endregion GET

    #region CREATE

    public void CreateVisit(Visit visit)
    {
        _allphiContext.Visit.Add(visit);
        _allphiContext.SaveChanges();
    }

    #endregion CREATE

    #region UPDATE

    public void UpdateVisit(Visit visit)
    {
        _allphiContext.Visit.Update(visit);
        _allphiContext.SaveChanges();
    }

    #endregion UPDATE
}