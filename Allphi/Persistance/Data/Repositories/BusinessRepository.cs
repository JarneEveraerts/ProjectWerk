using Domain.Models;
using Domain.Services;

namespace Persistance.Data.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly AllphiContext _allphiContext;

    public BusinessRepository(AllphiContext allphiContext)
    {
        _allphiContext = allphiContext;
    }

    #region GET

    public List<Business> GetBusinesses()
    {
        List<Business> businesses = _allphiContext.Business.Where(b => b.IsDeleted == false).ToList();
        return businesses;
    }

    public Business GetBusinessByName(string name)
    {
        Business business = _allphiContext.Business.FirstOrDefault(b => b.Name == name && b.IsDeleted == false);
        return business;
    }

    public Business GetBusinessById(int id)
    {
        Business business = _allphiContext.Business.FirstOrDefault(b => b.Id == id && b.IsDeleted == false);
        return business;
    }

    public Business GetBusinessByBTW(string btw)
    {
        Business business = _allphiContext.Business.FirstOrDefault(b => b.Btw == btw && b.IsDeleted == false);
        return business;
    }

    public Business GetBusinessByEmail(string email)
    {
        Business business = _allphiContext.Business.FirstOrDefault(b => b.Email == email && b.IsDeleted == false);
        return business;
    }

    #endregion GET

    #region CREATE

    public void CreateBusiness(Business business)
    {
        _allphiContext.Business.Add(business);
        _allphiContext.SaveChanges();
    }

    #endregion CREATE

    #region UPDATE

    public void UpdateBusiness(Business business)
    {
        _allphiContext.Business.Update(business);
        _allphiContext.SaveChanges();
    }

    #endregion UPDATE
}