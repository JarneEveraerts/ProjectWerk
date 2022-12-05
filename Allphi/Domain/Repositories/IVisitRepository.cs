using Domain.Models;

namespace Domain.Repositories;

public interface IVisitRepository
{
    #region GET

    Task<List<Visit>> GetVisits();

    List<Visit> GetVisitsByEmployee(int employeeId);

    List<Visit> GetVisitsByBusiness(int businessId);

    List<Visit> GetVisitsByVisitor(int visitorId);

    Visit GetVisitByVisitor(int visitorId);

    #endregion GET

    #region CREATE

    void CreateVisit(Visit visit);

    #endregion CREATE

    #region UPDATE

    void UpdateVisit(Visit visit);

    #endregion UPDATE
}