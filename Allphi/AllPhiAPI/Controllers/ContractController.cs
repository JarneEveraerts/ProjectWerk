using Domain.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(contracts);
        }

        [HttpGet("{id}", Name = "GetContractById")]
        public IActionResult GetContractById(int id)
        {
            var contract = _contractRepository.GetContractById(id);
            return Ok(contract);
        }

        // get by business
        [HttpGet("{business}", Name = "GetContractByBusiness")]
        public IActionResult GetContractByBusiness(Business business)
        {
            var contract = _contractRepository.GetContractByBusiness(business);
            return Ok(contract);
        }

        //get contract by endDate
        [HttpGet("{endDate}", Name = "GetContractByEndDate")]
        public IActionResult GetContractByEndDate(DateTime endDate)
        {
            var contract = _contractRepository.GetContractByEndDate(endDate);
            return Ok(contract);
        }

        //create contract
        [HttpPost("{Contract}", Name = "CreateContract")]
        public IActionResult CreateContract(Contract contract)
        {
            _contractRepository.CreateContract(contract);
            return Ok();
        }

        //update contract
        [HttpPut("{Contract}", Name = "UpdateContract")]
        public IActionResult UpdateContract(Contract contract)
        {
            _contractRepository.UpdateContract(contract);
            return Ok();
        }

    }
}
