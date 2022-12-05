using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IContractService
    {
        void CreateContract(CreateContractDTO contractDTO);
        void DeleteContract(int id);
        Contract GetContractByBusiness(Business business);
        Task<List<Contract>> GetContracts();
        void UpdateContract(UpdateContractDTO updateContractDTO);
    }
}