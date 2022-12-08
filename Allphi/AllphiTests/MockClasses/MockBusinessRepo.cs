using Domain.Models;
using Domain.Services;

namespace AllphiTests.MockClasses
{
    internal class MockBusinessRepo : IBusinessRepository
    {
         private List<Business> businessesList = new List<Business>();

        public void CreateBusiness(Business business)
        { 
            businessesList.Add(business);
            
            
        }

        public Business GetBusinessByBTW(string btw)
        {
            return businessesList.Find(b => b.Btw == btw);
        }

        public Business GetBusinessByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Business GetBusinessById(int id)
        {
            return businessesList[id];
        }

        public Business GetBusinessByName(string name)
        {
            return businessesList.Find(b => b.Name == name);
        }

        public List<Business> GetBusinesses()
        {
            return businessesList;
        }

        public void UpdateBusiness(Business business)
        {
            businessesList[business.Id] = business;
        }
    }
}