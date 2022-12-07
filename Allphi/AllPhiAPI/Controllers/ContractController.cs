using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("Contracts")]
    [ApiController]
    public class ContractController
    {
        private readonly IContractService _contractService;
        private readonly IContractBusinessService _contractBusinessService;

        public ContractController(IContractService contractService, IContractBusinessService contractBusinessService)
        {
            _contractService = contractService;
            _contractBusinessService = contractBusinessService;
        }

        #region GET
        [HttpGet]
        public async Task<List<Contract>> GetContracts()
        {
            return await _contractService.GetContracts();
        }

        [HttpPost("business/contract/{business}")]
        public Contract FetchContractByBusiness([FromRoute] Business business)
        {
            return _contractService.GetContractByBusiness(business);
        }

        #endregion  

        #region CREATE
        [HttpPut]
        public void CreateContract(CreateContractDTO contract)
        {
            _contractBusinessService.CreateContract(contract);
        }
        #endregion

        #region UPDATE
        [HttpPatch]
        public void UpdateContract(UpdateContractDTO contractDTO)
        {
            _contractService.UpdateContract(contractDTO);
        }

        [HttpDelete("{contractId}")]
        public void DeleteContract([FromRoute] int contractId)
        {
            _contractService.DeleteContract(contractId);
        }
        #endregion
    }

}