using Domain.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllPhiAPI.Controllers
{
    [Route("Contracts")]
    [ApiController]
    public class ContractController : Controller
    {
        //webapu using icontractrepository
        private readonly IContractRepository _contractRepository;

        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        // all get routes
        [HttpGet]
        public IActionResult GetContracts()
        {
            var contracts = _contractRepository.GetContracts();
            if (contracts.Count == 0) return NoContent();
            return Ok(contracts.Select(contract => new ContractDto(contract)));
        }

        [HttpGet("id/{id}", Name = "GetContractById")]
        public IActionResult GetContractById(int id)
        {
            var contract = _contractRepository.GetContractById(id);
            if (contract == null) return NoContent();
            return Ok(new ContractDto(contract));
        }

        // get by business
        [HttpGet("business/{business}", Name = "GetContractByBusiness")]
        public IActionResult GetContractByBusiness(Business business)
        {
            var contract = _contractRepository.GetContractByBusiness(business);
            if (contract == null) return NoContent();
            return Ok(new ContractDto(contract));
        }

        //get contract by endDate
        [HttpGet("enddate/{endDate}", Name = "GetContractByEndDate")]
        public IActionResult GetContractByEndDate(DateTime endDate)
        {
            var contract = _contractRepository.GetContractByEndDate(endDate);
            if (contract == null) return NoContent();
            return Ok(new ContractDto(contract));
        }

        //create contract
        [HttpPost]
        public IActionResult CreateContract(Contract contract)
        {
            _contractRepository.CreateContract(contract);
            return Ok();
        }

        //update contract
        [HttpPut]
        public IActionResult UpdateContract(Contract contract)
        {
            _contractRepository.UpdateContract(contract);
            return Ok();
        }
    }
}