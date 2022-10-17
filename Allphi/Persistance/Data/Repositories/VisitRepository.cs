using Domain.Models;
using Domain.Services;

namespace Persistance.Data.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AllphiContext _allphiContext;

    public VisitRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    #region GET

    public List<Visit> GetVisits()
    {
        List<Visit> visits = _allphiContext.Visit.ToList();
        return visits;
    }

    public List<Visit> GetVisitsByEmployee(Employee employee)
    {
        List<Visit> visits = _allphiContext.Visit.Where(v => v.Employee == employee).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByBusiness(Business business)
    {
        List<Visit> visits = _allphiContext.Visit.Where(v => v.Business == business).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByVisitor(Visitor visitor)
    {
        List<Visit> visits = _allphiContext.Visit.Where(v => v.Visitor == visitor).ToList();
        return visits;
    }

    public Visit GetVisitByVisitor(Visitor visitor)
    {
        Visit visit = _allphiContext.Visit.First(v => v.Visitor == visitor);
        return visit;
    }

    #endregion GET

    #region CREATE

    public void CreateVisit(Visit visit)
    {
        _allphiContext.Visit.Add(visit);
    }

    #endregion CREATE

    #region UPDATE

    public void UpdateVisit(Visit visit)
    {
        _allphiContext.Visit.Update(visit);
        _allphiContext.SaveChanges();
    }

    #endregion UPDATE

    #region DELETE

    public void DeleteVisit(Visit visit)
    {
        _allphiContext.Visit.Remove(visit);
        _allphiContext.SaveChanges();
    }

    #endregion DELETE
}