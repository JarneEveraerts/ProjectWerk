using Domain.Models;

namespace Domain.Services;

public interface IBusinessRepository
{
    #region GET

    List<Business> GetBusinesses();

    Business GetBusinessByName(string name);

    Business GetBusinessById(int id);

    Business GetBusinessByBTW(string btw);

    Business GetBusinessByEmail(string email);

    #endregion GET

    #region CREATE

    void CreateBusiness(Business business);

    #endregion CREATE

    #region UPDATE

    void UpdateBusiness(Business business);

    #endregion UPDATE
}