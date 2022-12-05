using Domain.Models;

namespace Domain.Repositories;

public interface IVisitorRepository
{
    #region GET

    Task<List<Visitor>> GetVisitors();

    Task<List<Visitor>> GetVisitorsByBusiness(string Business);

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