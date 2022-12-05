using Domain.Models;
using Domain.Services;

namespace AllphiTests.MockClasses
{
    internal class MockContractRepo : IContractRepository
    {
        List<Contract> contracts = new List<Contract>();
        public void CreateContract(Contract contract)
        {
            contracts.Add(contract);
        }

        public Contract GetContractByBusiness(Business business)
        {
            foreach (Contract item in contracts)
            {
                if (item.Business == business)
                {
                    return item;
                }
            }

            return null;
        }

        public Contract GetContractByEndDate(DateTime date)
        {
            foreach (Contract item in contracts)
            {
                if (item.EndDate == date)
                {
                    return item;
                }
            }
            return null;
        }

        public Contract GetContractById(int id)
        {
            return contracts[id];
        }

        public List<Contract> GetContracts()
        {
            return contracts;
        }

        public void UpdateContract(Contract contract)
        {
            contracts[contract.Id] = contract;
        }
    }
}