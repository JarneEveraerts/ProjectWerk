using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IContractBusinessService
    {
        void CreateContract(CreateContractDTO contract);
    }
}