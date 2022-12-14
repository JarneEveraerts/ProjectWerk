using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IVisitorBusinessEmployeeVisitService
    {
        void CreateVisit(CreateVisitDTO visitDTO);
        void UpdateVisit(UpdateVisitDTO visitDTO);
    }
}