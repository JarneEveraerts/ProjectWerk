using Domain.Models;

namespace Domain.Repositories;

public interface IContractRepository
{
    #region GET

    Task<List<Contract>> GetContracts();

    Contract GetContractByBusiness(Business business);

    Contract GetContractByEndDate(DateTime date);

    #endregion GET

    #region CREATE

    void CreateContract(Contract contract);

    #endregion CREATE

    #region UPDATE

    void UpdateContract(Contract contract);

    #endregion UPDATE

    Contract GetContractById(int id);
}