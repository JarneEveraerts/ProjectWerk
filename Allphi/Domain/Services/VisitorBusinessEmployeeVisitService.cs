using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class VisitorBusinessEmployeeVisitService : IVisitorBusinessEmployeeVisitService
    {
        private readonly IVisitorRepository visitorRepository;
        private readonly IVisitRepository visitRepository;
        private readonly IBusinessRepository businessRepository;
        private readonly IEmployeeRepository employeeRepository;

        public VisitorBusinessEmployeeVisitService(IVisitorRepository visitorRepository, IVisitRepository visitRepository, IBusinessRepository businessRepository, IEmployeeRepository employeeRepository)
        {
            this.visitorRepository = visitorRepository;
            this.visitRepository = visitRepository;
            this.businessRepository = businessRepository;
            this.employeeRepository = employeeRepository;
        }

        public void CreateVisit(CreateVisitDTO visitDTO)
        {
            Visit visit = new Visit();
            visit.Visitor = visitorRepository.GetVisitorByName(visitDTO.Visitor);
            visit.Employee = employeeRepository.GetEmployeeByName(visitDTO.Employee);
            visit.Business = businessRepository.GetBusinessByName(visitDTO.Business);
            visit.StartDate = DateTime.Now;
            visitRepository.CreateVisit(visit);
        }
        public void UpdateVisit(UpdateVisitDTO visitDTO)
        {
            Visit visit = visitRepository.GetVisitByVisitor(visitDTO.Visitor);
            visit.Visitor = visitorRepository.GetVisitorById(visitDTO.Visitor);
            visit.Employee = employeeRepository.GetEmployeeByName(visitDTO.Employee);
            visit.Business = businessRepository.GetBusinessByName(visitDTO.Business);
            visit.StartDate = visitDTO.Start;
            visit.EndDate = visitDTO.End;
            visitRepository.UpdateVisit(visit);
        }
    }
}
