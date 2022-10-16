using Domain.Models;

namespace Domain.Services;

public interface IVisitRepository
{
    #region GET

    List<Visit> GetVisits();

    List<Visit> GetVisitsByEmployee(Employee employee);

    List<Visit> GetVisitsByBusiness(Business business);

    List<Visit> GetVisitsByVisitor(Visitor visitor);

    Visit GetVisitByVisitor(Visitor visitor);

    #endregion GET

    #region CREATE

    void CreateVisit(Visit visit);

    #endregion CREATE

    #region UPDATE

    void UpdateVisit(Visit visit);

    #endregion UPDATE

    #region DELETE

    void DeleteVisit(Visit visit);

    #endregion DELETE
}