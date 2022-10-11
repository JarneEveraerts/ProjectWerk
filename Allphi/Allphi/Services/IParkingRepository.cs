using Allphi.Models;
using System.Collections.Generic;

namespace Allphi.Services
{
    public interface IParkingRepository
    {
        void AddParking(Parking parking);

        List<Parking> GetAllParking();
    }
}