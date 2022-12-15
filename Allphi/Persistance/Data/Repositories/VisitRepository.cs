using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AllphiContext _allphiContext;

    public VisitRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    #region GET

    public async Task<List<Visit>> GetVisits()
    {
        List<Visit> visits = await _allphiContext.Visit.Where(v => v.IsDeleted == false)
            .Include(v => v.Business)
            .Include(v => v.Employee).ThenInclude(e => e.Business)
            .Include(v => v.Visitor)
            .ToListAsync();
        return visits;
    }

    public List<Visit> GetVisitsByEmployee(int employeeId)
    {

        List<Visit> visits = _allphiContext.Visit.Where(v => v.Employee.Id == employeeId).Include(v => v.Business)
            .Include(v => v.Employee).ThenInclude(e => e.Business)
            .Include(v => v.Visitor).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByBusiness(int businessId)
    {
        List<Visit> visits = _allphiContext.Visit.Where(v => v.Business.Id == businessId).Include(v => v.Business)
            .Include(v => v.Employee).ThenInclude(e => e.Business)
            .Include(v => v.Visitor).ToList();
        return visits;
    }

    public List<Visit> GetVisitsByVisitor(int visitorId)
    {
        List<Visit> visits = _allphiContext.Visit.Where(v => v.Visitor.Id == visitorId).Include(v => v.Business)
            .Include(v => v.Employee).ThenInclude(e => e.Business)
            .Include(v => v.Visitor).ToList();
        return visits;
    }

    public Visit GetVisitByVisitor(int visitorId)
    {
        Visit visit = _allphiContext.Visit.OrderByDescending(v => v.EndDate).Include(v => v.Business)
            .Include(v => v.Employee).ThenInclude(e => e.Business)
            .Include(v => v.Visitor).FirstOrDefault(v => v.Visitor.Id == visitorId);
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