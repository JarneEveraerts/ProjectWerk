using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(visits);
        }

        //get visits by employee
        [HttpGet("{employee}", Name = "GetVisitsByEmployee")]
        public IActionResult GetVisitsByEmployee(Employee employee)
        {
            var visits = _visitRepository.GetVisitsByEmployee(employee);
            return Ok(visits);
        }

        //get visits by business
        [HttpGet("{business}", Name = "GetVisitsByBusiness")]
        public IActionResult GetVisitsByBusiness(Business business)
        {
            var visits = _visitRepository.GetVisitsByBusiness(business);
            return Ok(visits);
        }

        //get visits by visitor
        [HttpGet("{visitsvisitor}", Name = "GetVisitsByVisitor")]
        public IActionResult GetVisitsByVisitor(Visitor visitor)
        {
            var visits = _visitRepository.GetVisitsByVisitor(visitor);
            return Ok(visits);
        }

        //get visit by visitor
        [HttpGet("{visitor}", Name = "GetVisitByVisitor")]
        public IActionResult GetVisitByVisitor(Visitor visit)
        {
            var visits = _visitRepository.GetVisitByVisitor(visit);
            return Ok(visits);
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
