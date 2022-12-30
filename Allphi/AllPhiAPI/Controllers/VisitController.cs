using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace AllPhiAPI.Controllers
{
    [Route("Visits")]
    [ApiController]
    public class VisitController : Controller
    {
        //webapi using ivisitrepository
        private readonly IVisitRepository _visitRepository;

        public VisitController(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        // all get routes

        [HttpGet]
        public IActionResult GetVisits()
        {
            var visits = _visitRepository.GetVisits();
            if (visits.Count == 0) return NoContent();
            return Ok(visits.Select(v => new VisitDto(v)));
        }

        //get visits by employee
        [HttpGet("employee/{employee}", Name = "GetVisitsByEmployee")]
        public IActionResult GetVisitsByEmployee(Employee employee)
        {
            var visits = _visitRepository.GetVisitsByEmployee(employee);
            if (visits.Count == 0) return NoContent();
            return Ok(visits.Select(v => new VisitDto(v)));
        }

        //get visits by business
        [HttpGet("business/{business}", Name = "GetVisitsByBusiness")]
        public IActionResult GetVisitsByBusiness(Business business)
        {
            var visits = _visitRepository.GetVisitsByBusiness(business);
            if (visits.Count == 0) return NoContent();
            return Ok(visits.Select(v => new VisitDto(v)));
        }

        //get visits by visitor
        [HttpGet("visits/{visitor}", Name = "GetVisitsByVisitor")]
        public IActionResult GetVisitsByVisitor(Visitor visitor)
        {
            var visits = _visitRepository.GetVisitsByVisitor(visitor);
            if (visits.Count == 0) return NoContent();
            return Ok(visits.Select(v => new VisitDto(v)));
        }

        //get visit by visitor
        [HttpGet("visit/{visitor}", Name = "GetVisitByVisitor")]
        public IActionResult GetVisitByVisitor(Visitor visitor)
        {
            var visit = _visitRepository.GetVisitByVisitor(visitor);
            if (visit == null) return NoContent();
            return Ok(new VisitDto(visit));
        }

        //create
        [HttpPost]
        public IActionResult CreateVisit([FromBody] Visit visit)
        {
            _visitRepository.CreateVisit(visit);
            return CreatedAtRoute("GetVisitByVisitor", new { visitor = visit.Visitor }, visit);
        }

        //update
        [HttpPut("{id}")]
        public IActionResult UpdateVisit(Visit visit)
        {
            _visitRepository.UpdateVisit(visit);
            return NoContent();
        }
    }
}