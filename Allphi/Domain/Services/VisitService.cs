using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;

namespace Domain.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private HttpClient _apiClient;

        public VisitService(IVisitRepository VisitRepository)
        {
            _visitRepository = VisitRepository;
        }

        public Task<List<Visit>> GetVisits()
        {
            return _visitRepository.GetVisits();
        }
        public List<Visit> GetVisitsByEmployee(int employeeId)
        {
            return _visitRepository.GetVisitsByEmployee(employeeId);
        }
        public List<Visit> GetVisitsByBusiness(int businessId)
        {
            return _visitRepository.GetVisitsByBusiness(businessId);
        }
        public List<Visit> GetVisitsByVisitor(int visitorId)
        {
            return _visitRepository.GetVisitsByVisitor(visitorId);
        }
        public Visit GetVisitByVisitor(int visitorId)
        {
            return _visitRepository.GetVisitByVisitor(visitorId);
        }
        public void UpdateVisit(UpdateVisitDTO visitDTO)
        {
            Visit visit = _visitRepository.GetVisitByVisitor(visitDTO.Visitor.Id);
            visit.Visitor = visitDTO.Visitor;
            visit.Employee = visitDTO.Employee;
            visit.Business = visitDTO.Business;
            visit.StartDate = visitDTO.Start;
            visit.EndDate = visitDTO.End;
            _visitRepository.UpdateVisit(visit);
        }

        public void DeleteVisit(int visitorId)
        {
            Visit visit = _visitRepository.GetVisitByVisitor(visitorId);
            visit.IsDeleted = true;
            _visitRepository.UpdateVisit(visit);
        }

        public void CreateVisit(CreateVisitDTO visitDTO)
        {
            Visit visit = new Visit();
            visit.Visitor = visitDTO.Visitor;
            visit.Employee = visitDTO.Employee;
            visit.Business = visitDTO.Business;
            visit.StartDate = DateTime.Now;
            _visitRepository.CreateVisit(visit);
        }

    }
}
