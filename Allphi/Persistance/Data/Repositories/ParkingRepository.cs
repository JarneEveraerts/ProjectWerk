using Domain.Models;
using Domain.Services;
using Persistance.Data;
using System.Collections.Generic;
using System.Linq;

namespace Persistance.Data.Repositories
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly AllphiContext _context;

        public ParkingRepository()
        {
            _context = new AllphiContext();
        }

        public void AddParking(Parking? parking)
        {
            _context.Parking.Add(parking);
            _context.SaveChanges();
        }

        public List<Parking> GetAllParking()
        {
            List<Parking> parkingList = _context.Parking.ToList();
            return parkingList;
        }
    }
}