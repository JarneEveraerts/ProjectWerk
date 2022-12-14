using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("Visits")]
    [ApiController]
    public class VisitController
    {
        private readonly IVisitService _visitService;
        private readonly IVisitorBusinessEmployeeVisitService visitorBusinessEmployeeVisitService;

        public VisitController(IVisitService visitService, IVisitorBusinessEmployeeVisitService visitorBusinessEmployeeVisitService)
        {
            _visitService = visitService;
            this.visitorBusinessEmployeeVisitService = visitorBusinessEmployeeVisitService;
        }
        #region GET

        [HttpGet]
        public async Task<List<Visit>> GetVisits()
        {
            {
                return await _visitService.GetVisits();
            }
        }
        #endregion

        [HttpGet("employee/{employeeId}")]
        public List<Visit> GetVisitsByEmployee(int employeeId)
        {
            return _visitService.GetVisitsByEmployee(employeeId);
        }
        [HttpGet("business/{businessId}")]
        public List<Visit> GetVisitsByBusiness(int businessId)
        {
            return _visitService.GetVisitsByBusiness(businessId);
        }

        [HttpGet("visitor/{visitorId}")]
        public List<Visit> GetVisitsByVisitor(int visitorId)
        {
            return _visitService.GetVisitsByVisitor(visitorId);
        }

        [HttpPatch]
        public void UpdateVisit(UpdateVisitDTO visitDTO)
        {
            visitorBusinessEmployeeVisitService.UpdateVisit(visitDTO);
        }

        [HttpDelete("{visitorId}")]
        public void DeleteVisit(int visitorId)
        {
            _visitService.DeleteVisit(visitorId);
        }

        [HttpPut]
        public void CreateVisit(CreateVisitDTO visitDTO)
        {
            visitorBusinessEmployeeVisitService.CreateVisit(visitDTO);
        }


    }
}
