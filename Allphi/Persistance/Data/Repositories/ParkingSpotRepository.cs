using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistance.Data.Repositories
{
    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly AllphiContext _allphiContext;

        public ParkingSpotRepository(AllphiContext allphiContext)
        {
            _allphiContext = allphiContext;
        }

        private IQueryable<ParkingSpot> ParkingSpotStandard()
        {
            return _allphiContext.ParkingSpot.Include(p => p.Reserved).Include(p => p.Employee).Include(p => p.Visitor);
        }

        #region GET

        public List<ParkingSpot> GetParkingSpots()
        {
            List<ParkingSpot> parkingSpots = ParkingSpotStandard().Where(p => p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public List<ParkingSpot> GetParkingSpotsByReserved(Business reserved)
        {
            List<ParkingSpot> parkingSpots = ParkingSpotStandard().Where(p => p.Reserved == reserved && p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsReserved(Business reserved)
        {
            List<ParkingSpot> parkingSpots = ParkingSpotStandard().Where(p => p.Reserved == reserved && p.Plate == null && p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public ParkingSpot GetAvailableParkingSpotReserved(Business reserved)
        {
            ParkingSpot parkingSpot = ParkingSpotStandard().FirstOrDefault(p => p.Reserved == reserved && p.Plate == null && p.IsDeleted == false);
            return parkingSpot;
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsByPlate()
        {
            List<ParkingSpot> parkingSpots =
                ParkingSpotStandard().Where(p => p.Plate == null && p.IsDeleted == false).ToList();
            return parkingSpots;
        }

        public ParkingSpot? GetParkingSpotByPlate(string? licensePlate)
        {
            ParkingSpot? parkingSpot =
                ParkingSpotStandard().FirstOrDefault(p => p.Plate == licensePlate && p.IsDeleted == false);
            return parkingSpot;
        }

        public ParkingSpot GetParkingSpotByEmployee(Employee employee)
        {
            ParkingSpot parkingSpot = ParkingSpotStandard().FirstOrDefault(p => p.Employee == employee && p.IsDeleted == false);
            return parkingSpot;
        }

        public ParkingSpot GetParkingSpotByVisitor(Visitor visitor)
        {
            ParkingSpot parkingSpot = ParkingSpotStandard().FirstOrDefault(p => p.Visitor == visitor && p.IsDeleted == false);
            return parkingSpot;
        }

        public ParkingSpot? GetAvailableParkingSpotUnreserved()
        {
            ParkingSpot parkingSpot = ParkingSpotStandard().FirstOrDefault(p => p.Reserved == null && p.Plate == null && p.IsDeleted == false);
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