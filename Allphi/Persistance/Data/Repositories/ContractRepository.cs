using Domain.Models;
using Domain.Services;

namespace Persistance.Data.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly AllphiContext _allphiContext;

    public ContractRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    #region GET

    public List<Contract> GetContracts()
    {
        List<Contract> contracts = _allphiContext.Contract.ToList();
        return contracts;
    }

    public Contract GetContractByBusiness(Business business)
    {
        Contract contract = _allphiContext.Contract.First(c => c.Business == business);
        return contract;
    }

    public Contract GetContractByEndDate(DateTime date)
    {
        Contract contract = _allphiContext.Contract.First(c => c.EndDate == date);
        return contract;
    }

    #endregion GET

    #region CREATE

    public void CreateContract(Contract contract)
    {
        _allphiContext.Contract.Add(contract);
        _allphiContext.SaveChanges();
    }

    #endregion CREATE

    #region Update

    public void UpdateContract(Contract contract)
    {
        _allphiContext.Contract.Update(contract);
        _allphiContext.SaveChanges();
    }

    #endregion Update

    #region Delete

    public void DeleteContract(Contract contract)
    {
        _allphiContext.Contract.Remove(contract);
        _allphiContext.SaveChanges();
    }

    #endregion Delete
}