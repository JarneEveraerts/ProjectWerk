using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IVisitorService
    {
        Visitor CreateVisitor(CreateVisitorDTO visitorDTO);
        void CreateVisitorBalie(CreateVisitorBalieDTO visitorDTO);
        void CreateVisitorWithPlate(CreateVisitorWithPlateDTO visitorWithPlateDTO);
        void DeleteVisitor(int id);
        Visitor GetVisitorById(int id);
        Visitor GetVisitorByMail(string email);
        Visitor GetVisitorByName(string name);
        Task<List<Visitor>> GetVisitors();
        Task<List<Visitor>> GetVisitorsByBusiness(string business);
        void UpdateVisitor(UpdateVisitorDTO updateVisitorDTO, int id);
    }
}