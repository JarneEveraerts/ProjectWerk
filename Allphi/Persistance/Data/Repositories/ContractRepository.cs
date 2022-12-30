using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly AllphiContext _allphiContext;

    public ContractRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    private IQueryable<Contract> ContractStandard()
    {
        return _allphiContext.Contract.Include(c => c.Business);
    }
    #region GET

    public List<Contract> GetContracts()
    {
        List<Contract> contracts = ContractStandard().Where(c => c.IsDeleted == false).ToList();
        return contracts;
    }

    public Contract GetContractByBusiness(Business business)
    {
        Contract contract = ContractStandard().FirstOrDefault(c => c.Business == business && c.IsDeleted == false);
        return contract;
    }

    public Contract GetContractById(int id)
    {
        return ContractStandard().First(c => c.Id == id && c.IsDeleted == false);
    }

    public Contract GetContractByEndDate(DateTime date)
    {
        Contract contract = ContractStandard().FirstOrDefault(c => c.EndDate == date && c.IsDeleted == false);
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