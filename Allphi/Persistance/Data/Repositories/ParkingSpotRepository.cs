using Domain.Models;
using Domain.Services;
using Persistance.Data;
using System.Collections.Generic;
using System.Linq;

namespace Persistance.Data.Repositories
{
    public class ParkingSpotRepository : IParkingSpotsRepository
    {
        private readonly AllphiContext _context;

        public ParkingSpotRepository()
        {
            _context = new AllphiContext();
        }

        public void AddParking(ParkingSpot? parking)
        {
            _context.Parking.Add(parking);
            _context.SaveChanges();
        }

        public List<ParkingSpot> GetAllParking()
        {
            List<ParkingSpot> parkingList = _context.Parking.ToList();
            return parkingList;
        }
    }
}