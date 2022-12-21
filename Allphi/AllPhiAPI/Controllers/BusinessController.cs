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
            return Ok(businesses.Select(b => new BusinessDto(b)));
        }

        [HttpGet("{id}", Name = "GetBusinessById")]
        public IActionResult GetBusinessById(int id)
        {
            var business = _businessRepository.GetBusinessById(id);
            return Ok(new BusinessDto(business));
        }

        //get by name
        [HttpGet("{name}", Name = "GetBusinessByName")]
        public IActionResult GetBusinessByName(string name)
        {
            var business = _businessRepository.GetBusinessByName(name);
            return Ok(new BusinessDto(business));
        }

        //create using business
        [HttpPost("{Business}", Name = "CreateBusiness")]
        public IActionResult CreateBusiness(Business business)
        {
            _businessRepository.CreateBusiness(business);
            return Ok();
        }

        //update using business
        [HttpPut("{Business}", Name = "UpdateBusiness")]
        public IActionResult UpdateBusiness(Business business)
        {
            _businessRepository.UpdateBusiness(business);
            return Ok();
        }
    }
}