using System.Collections.Generic;
using Allphi.Data;
using Allphi.Models;

namespace Allphi.Services;

public interface IVisitorRepository
{
    Visitor GetVisitor(string email);

    List<Visitor> GetVisitors();

    void CreateVisitor(Visitor visitor);
}