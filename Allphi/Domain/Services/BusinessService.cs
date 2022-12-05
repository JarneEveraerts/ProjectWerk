using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;

namespace Domain.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepo;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepo = businessRepository;
        }

        public Task<List<Business>> GetBusinesses()
        {
            return _businessRepo.GetBusinesses();
        }
        public Business GetBusinessById(int id)
        {
            return _businessRepo.GetBusinessById(id);
        }
        public Business GetBusinessByName(string business)
        {
            return _businessRepo.GetBusinessByName(business);
        }
        public Business GetBusinessByEmail(string email)
        {
            return _businessRepo.GetBusinessByEmail(email);
        }
        public async Task<List<List<string>>> GiveBusinesses()
        {
            var business = await GetBusinesses();
            return business.Select(business => new List<string>() { business.Name, business.Btw, business.Email, business.Address, business.Phone, business.IsDeleted.ToString() }).ToList();
        }
        public Business GetBusinessByBtw(string btw)
        {
            return _businessRepo.GetBusinessByBTW(btw);
        }
        public void UpdateBusiness(CreateAndUpdateBusinessDTO updateBusinessDTO, int id)
        {
            Business business = _businessRepo.GetBusinessById(id);

            business.Name = updateBusinessDTO.Name;
            business.Address = updateBusinessDTO.Address;
            business.Phone = updateBusinessDTO.Phone;
            business.Email = updateBusinessDTO.Email;
            business.Btw = updateBusinessDTO.Btw;

            _businessRepo.UpdateBusiness(business);
        }
        public void DeleteBusiness(int id)
        {
            Business business = _businessRepo.GetBusinessById(id);
            business.IsDeleted = true;
            _businessRepo.UpdateBusiness(business);
        }
        public void CreateBusiness(CreateAndUpdateBusinessDTO businessDTO)
        {
            if (_businessRepo.GetBusinessByBTW(businessDTO.Btw) == null)
            {
                _businessRepo.CreateBusiness(new Business
                {
                    Name = businessDTO.Name,
                    Address = businessDTO.Address,
                    Phone = businessDTO.Phone,
                    Email = businessDTO.Email,
                    Btw = businessDTO.Btw
                });
            }
            else
            {
                throw new Exception("Business already exists");
            }
        }
    }
}
