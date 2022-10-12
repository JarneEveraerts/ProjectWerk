using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services;

public interface IVisitorRepository
{
    Visitor GetVisitor(string email);

    List<Visitor> GetVisitors();

    void CreateVisitor(Visitor visitor);
}