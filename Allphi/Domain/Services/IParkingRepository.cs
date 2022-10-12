using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IParkingRepository
    {
        void AddParking(Parking parking);

        List<Parking> GetAllParking();
    }
}