using Domain.Models;
using Domain.Services;

namespace Persistance.Data.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly AllphiContext _allphiContext;

    public BusinessRepository()
    {
        _allphiContext = new();
    }

    #region GET

    public List<Business> GetBusinesses()
    {
        List<Business> businesses = _allphiContext.Business.ToList();
        return businesses;
    }

    public Business GetBusinessByName(string name)
    {
        Business business = _allphiContext.Business.First(b => b.Name == name);
        return business;
    }

    public Business GetBusinessById(int id)
    {
        Business business = _allphiContext.Business.First(b => b.Id == id);
        return business;
    }

    public Business GetBusinessByBTW(string btw)
    {
        Business business = _allphiContext.Business.First(b => b.Btw == btw);
        return business;
    }

    public Business GetBusinessByEmail(string email)
    {
        Business business = _allphiContext.Business.First(b => b.Email == email);
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

    #region DELETE

    public void DeleteBusiness(Business business)

    {
        _allphiContext.Remove(business);
    }

    #endregion DELETE
}