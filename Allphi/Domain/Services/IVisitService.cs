using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IVisitService
    {
        void CreateVisit(CreateVisitDTO visitDTO);
        void DeleteVisit(int visitorId);
        Visit GetVisitByVisitor(int visitorId);
        Task<List<Visit>> GetVisits();
        List<Visit> GetVisitsByBusiness(int businessId);
        List<Visit> GetVisitsByEmployee(int employeeId);
        List<Visit> GetVisitsByVisitor(int visitorId);
        void UpdateVisit(UpdateVisitDTO visitDTO);
    }
}