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
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            this._contractRepository = contractRepository;
        }
        public Contract GetContractByBusiness(Business business)
        {
            return _contractRepository.GetContractByBusiness(business);
        }

        public void UpdateContract(UpdateContractDTO updateContractDTO)
        {
            Contract contract = _contractRepository.GetContractById(updateContractDTO.Id);
            contract.Business = updateContractDTO.Business;
            contract.TotalSpaces = updateContractDTO.Spots;
            contract.StartDate = updateContractDTO.Start;
            contract.EndDate = updateContractDTO.End;
            _contractRepository.UpdateContract(contract);
        }
        public void DeleteContract(int id)
        {
            Contract contract = _contractRepository.GetContractById(id);
            contract.IsDeleted = true;
            _contractRepository.UpdateContract(contract);
        }

        public void CreateContract(CreateContractDTO contractDTO)
        {
            _contractRepository.CreateContract(new Contract(
                contractDTO.Business,
                contractDTO.StartDate,
                contractDTO.EndDate,
                contractDTO.TotalSpaces));
        }
        public Task<List<Contract>> GetContracts()
        {
            return _contractRepository.GetContracts();
        }




    }
}
