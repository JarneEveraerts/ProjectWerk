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
    public class ContractBusinessService : IContractBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IContractRepository _contractRepository;

        public ContractBusinessService(IBusinessRepository businessRepository, IContractRepository contractRepository)
        {
            this._businessRepository = businessRepository;
            this._contractRepository = contractRepository;
        }

        public void CreateContract(CreateContractDTO contract)
        {
            var business = _businessRepository.GetBusinessById(contract.Business.Id);
            contract.Business = business;
            _contractRepository.CreateContract(new Contract(
                contract.Business,
                contract.StartDate,
                contract.EndDate,
                contract.TotalSpaces));
        }
    }
}
