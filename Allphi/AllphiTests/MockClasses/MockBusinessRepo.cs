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
            throw new NotImplementedException();
        }

        public Business GetBusinessByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Business GetBusinessById(int id)
        {
            throw new NotImplementedException();
        }

        public Business GetBusinessByName(string name)
        {
            return new Business("testbedrijf", "testbtw", "test");
            /*if (null != name)
            {
                return new Business(name, "test", "test");
            }
            throw new NullReferenceException("Business is null");*/
        }

        public List<Business> GetBusinesses()
        {
            return businessesList;
        }

        public void UpdateBusiness(Business business)
        {
            throw new NotImplementedException();
        }
    }
}