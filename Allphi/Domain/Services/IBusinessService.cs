using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IBusinessService
    {
        void CreateBusiness(CreateAndUpdateBusinessDTO businessDTO);
        void DeleteBusiness(int id);
        Business GetBusinessByBtw(string btw);
        Business GetBusinessByEmail(string email);
        Business GetBusinessById(int id);
        Business GetBusinessByName(string business);
        Task<List<Business>> GetBusinesses();
        Task<List<List<string>>> GiveBusinesses();
        void UpdateBusiness(CreateAndUpdateBusinessDTO updateBusinessDTO, int id);
    }
}