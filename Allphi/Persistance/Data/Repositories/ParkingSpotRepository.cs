using Domain.Models;
using Domain.Services;

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

        public List<ParkingSpot> GetParkingSpots()
        {
            List<ParkingSpot> parkingSpots = _allphiContext.ParkingSpot.ToList();
            return parkingSpots;
        }

        public List<ParkingSpot> GetParkingSpotsByReserved(Business reserved)
        {
            List<ParkingSpot> parkingSpots = _allphiContext.ParkingSpot.Where(p => p.Reserved == reserved).ToList();
            return parkingSpots;
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsByReserved(Business reserved)
        {
            List<ParkingSpot> parkingSpots = _allphiContext.ParkingSpot.Where(p => p.Reserved == reserved && p.Plate == null).ToList();
            return parkingSpots;
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsByPlate()
        {
            List<ParkingSpot> parkingSpots = _allphiContext.ParkingSpot.Where(p => p.Plate == null).ToList();
            return parkingSpots;
        }

        public ParkingSpot GetParkingSpotByPlate(string? licensePlate)
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.First(p => p.Plate == licensePlate);
            return parkingSpot;
        }

        public ParkingSpot GetParkingSpotByEmployee(Employee employee)
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.First(p => p.Employee == employee);
            return parkingSpot;
        }

        public ParkingSpot GetParkingSpotByVisitor(Visitor visitor)
        {
            ParkingSpot parkingSpot = _allphiContext.ParkingSpot.First(p => p.Visitor == visitor);
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
        }

        #endregion UPDATE

        #region DELETE

        public void DeleteParkingSpot(ParkingSpot parkingSpot)
        {
            _allphiContext.ParkingSpot.Remove(parkingSpot);
        }

        #endregion DELETE
    }
}