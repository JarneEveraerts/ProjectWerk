using Contracts.DTO;
namespace Contracts.Services;

public interface IVisitorRepository
{
    #region GET

    List<Visitor> GetVisitors();

    List<Visitor> GetVisitorsByBusiness(string Business);

    Visitor GetVisitorByName(string name);

    Visitor GetVisitorById(int id);

    Visitor GetVisitorByMail(string mail);

    #endregion GET

    #region CREATE

    int CreateVisitor(Visitor visitor);

    #endregion CREATE

    #region UPDATE

    void UpdateVisitor(Visitor visitor);

    #endregion UPDATE
}