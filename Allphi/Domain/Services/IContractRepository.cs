using Domain.Models;

namespace Domain.Services;

public interface IContractRepository
{
    #region GET

    List<Contract> GetContract();

    Contract GetContractByBusiness(Business business);

    Contract GetContractByEndDate(DateTime date);

    #endregion GET

    #region CREATE

    void CreateContract(Contract contract);

    #endregion CREATE

    #region UPDATE

    void UpdateContract(Contract contract);

    #endregion UPDATE

    #region DELETE

    void DeleteContract(Contract contract);

    #endregion DELETE
}