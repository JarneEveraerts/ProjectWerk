using Domain.Models;
using Domain.Repositories;

namespace AllphiTests.MockClasses
{
    internal class MockContractRepo : IContractRepository
    {
        public void CreateContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public Contract GetContractByBusiness(Business business)
        {
            throw new NotImplementedException();
        }

        public Contract GetContractByEndDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Contract GetContractById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contract> GetContracts()
        {
            throw new NotImplementedException();
        }

        public void UpdateContract(Contract contract)
        {
            throw new NotImplementedException();
        }
    }
}