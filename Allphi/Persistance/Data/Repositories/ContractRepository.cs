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
        List<Contract> contracts = _allphiContext.Contract.Where(c => c.IsDeleted == false).ToList();
        return contracts;
    }

    public Contract GetContractByBusiness(Business business)
    {
        Contract contract = _allphiContext.Contract.FirstOrDefault(c => c.Business == business);
        return contract;
    }

    public Contract GetContractById(int id)
    {
        return _allphiContext.Contract.First(c => c.Id == id);
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
}