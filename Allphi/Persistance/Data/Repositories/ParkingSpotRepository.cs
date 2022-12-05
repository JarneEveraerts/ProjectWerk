using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Repositories
{
    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly AllphiContext _allphiContext;

        public ParkingSpotRepository(AllphiContext allphiContext)
        {
            _allphiContext = allphiContext;
        }

        #region GET

        public async Task<List<ParkingSpot>> GetParkingSpots()
        {
            List<ParkingSpot> parkingSpots = await _allphiContext.ParkingSpot.Where(p => p.IsDeleted == false).ToListAsync();
            return parkingSpots;
        }

        public List<ParkingSpot> GetParkingSpotsByReserved(Business reserved)
        {
            List<ParkingSpot> parkingSpots = _allphiContext.ParkingSpot.Where(p => p.Reserved == reserved && p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsReserved(Business reserved)
        {
            List<ParkingSpot> parkingSpots = _allphiContext.ParkingSpot.Where(p => p.Reserved == reserved && p.Plate == null && p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public ParkingSpot GetAvailableParkingSpotReserved(Business reserved)
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.FirstOrDefault(p => p.Reserved == reserved && p.Plate == null && p.IsDeleted == false);
            return parkingSpot;
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsByPlate()
        {
            List<ParkingSpot> parkingSpots =
                _allphiContext.ParkingSpot.Where(p => p.Plate == null && p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public ParkingSpot? GetParkingSpotByPlate(string? licensePlate)
        {
            ParkingSpot? parkingSpot =
                _allphiContext.ParkingSpot.FirstOrDefault(p => p.Plate == licensePlate && p.IsDeleted == false);
            return parkingSpot;
        }

        public ParkingSpot GetParkingSpotByEmployee(Employee employee)
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.FirstOrDefault(p => p.Employee == employee && p.IsDeleted == false);
            return parkingSpot;
        }

        public ParkingSpot GetParkingSpotByVisitor(Visitor visitor)
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.FirstOrDefault(p => p.Visitor == visitor && p.IsDeleted == false);
            return parkingSpot;
        }

        public ParkingSpot? GetAvailableParkingSpotUnreserved()
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.FirstOrDefault(p => p.Reserved == null && p.Plate == null && p.IsDeleted == false);
            return parkingSpot;
        }

        #endregion GET

        #region CREATE

        public void CreateParkingSpot(ParkingSpot parkingSpot)
        {
            _allphiContext.ParkingSpot.Add(parkingSpot);
            _allphiContext.SaveChanges();
        }

        #endregion CREATE

        #region UPDATE

        public void UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            _allphiContext.ParkingSpot.Update(parkingSpot);
            _allphiContext.SaveChanges();
        }

        #endregion UPDATE

        #region COUNT

        public int CountParkingSpotByPlate(string plate)
        {
            return _allphiContext.ParkingSpot.Count(p => p.Plate == plate);
        }

        public bool ParkingSpotExist(string plate)
        {
            if (_allphiContext.ParkingSpot.FirstOrDefault(p => p.Plate == plate) != null) return true;
            return false;
        }

        #endregion COUNT
    }
}