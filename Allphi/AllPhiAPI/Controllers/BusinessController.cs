using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace AllPhiAPI.Controllers
{
    [ApiController]
    [Route("Businesses")]
    public class BusinessController : Controller
    {
        private readonly IBusinessRepository _businessRepository;

        //webapi using ibusinessrepository
        public BusinessController(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        // all get routes
        [HttpGet]
        public IActionResult GetBusinesses()
        {
            var businesses = _businessRepository.GetBusinesses();
            if (businesses.Count == 0) return NoContent();
            return Ok(businesses.Select(b => new BusinessDto(b)));
        }

        [HttpGet("id/{id}", Name = "GetBusinessById")]
        public IActionResult GetBusinessById(int id)
        {
            var business = _businessRepository.GetBusinessById(id);
            if (business == null) return NoContent();
            return Ok(new BusinessDto(business));
        }

        //get by name
        [HttpGet("name/{name}", Name = "GetBusinessByName")]
        public IActionResult GetBusinessByName(string name)
        {
            var business = _businessRepository.GetBusinessByName(name);
            if (business == null) return NoContent();
            return Ok(new BusinessDto(business));
        }

        //get by employee name
        [HttpGet("employee/{employeeName}", Name = "GetBusinessByEmployeeName")]
        public IActionResult GetBusinessByEmployeeName(string employeeName)
        {
            var business = _businessRepository.GetBusinessByEmployeeName(employeeName);
            if (business == null) return NoContent();
            return Ok(new BusinessDto(business));
        }

        //create using business
        [HttpPost]
        public IActionResult CreateBusiness(Business business)
        {
            _businessRepository.CreateBusiness(business);
            return Ok();
        }

        //update using business
        [HttpPut]
        public IActionResult UpdateBusiness(Business business)
        {
            _businessRepository.UpdateBusiness(business);
            return Ok();
        }
    }
}