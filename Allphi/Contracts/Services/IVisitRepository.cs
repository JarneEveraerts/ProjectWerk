using Contracts.DTO;
namespace Contracts.Services;

public interface IVisitRepository
{
    #region GET

    List<Contracts.DTO.Visit> GetVisits();

    List<Contracts.DTO.Visit> GetVisitsByEmployee(Contracts.DTO.Employee employee);

    List<Contracts.DTO.Visit> GetVisitsByBusiness(Contracts.DTO.Business business);

    List<Contracts.DTO.Visit> GetVisitsByVisitor(Contracts.DTO.Visitor visitor);

    Contracts.DTO.Visit GetVisitByVisitor(Contracts.DTO.Visitor visitor);

    #endregion GET

    #region CREATE

    void CreateVisit(Visit visit);

    #endregion CREATE

    #region UPDATE

    void UpdateVisit(Visit visit);

    #endregion UPDATE
}