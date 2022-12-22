using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace AllPhiAPI.Controllers
{
    [Route("Visitors")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly IVisitorRepository _visitorRepository;

        public VisitorController(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        // all get routes
        [HttpGet]
        public IActionResult GetVisitors()
        {
            var visitors = _visitorRepository.GetVisitors();
            return Ok(visitors);
        }

        [HttpGet("id/{id}", Name = "GetVisitorById")]
        public IActionResult GetVisitorById(int id)
        {
            var visitor = _visitorRepository.GetVisitorById(id);
            return Ok(new VisitorDto(visitor));
        }

        //get visitors by business
        [HttpGet("/business/{business}", Name = "GetVisitorsByBusiness")]
        public IActionResult GetVisitorsByBusiness(string business)
        {
            var visitor = _visitorRepository.GetVisitorsByBusiness(business);
            return Ok(visitor);
        }

        //get visitor by name
        [HttpGet("/name/{name}", Name = "GetVisitorByName")]
        public IActionResult GetVisitorByName(string name)
        {
            var visitor = _visitorRepository.GetVisitorByName(name);
            return Ok(visitor);
        }

        //get visitor by mail
        [HttpGet("/mail/{mail}", Name = "GetVisitorByMail")]
        public IActionResult GetVisitorByMail(string mail)
        {
            var visitor = _visitorRepository.GetVisitorByMail(mail);
            return Ok(visitor);
        }

        //create
        [HttpPost]
        public IActionResult CreateVisitor(Visitor visitor)
        {
            _visitorRepository.CreateVisitor(visitor);
            return Ok();
        }

        //update
        [HttpPut]
        public IActionResult UpdateVisitor(Visitor visitor)
        {
            _visitorRepository.UpdateVisitor(visitor);
            return Ok();
        }
    }
}