using Domain.Models;
using Domain.Repositories;


namespace AllphiTests.MockClasses
    class MockParkingSpotRepo : IParkingSpotRepository
    {
        List<ParkingSpot> parkingSpots = new List<ParkingSpot>();

        public int CountParkingSpotByPlate(string plate)
        {
            return parkingSpots.Count(p => p.Plate == plate);
        }

        public void CreateParkingSpot(ParkingSpot parkingSpot)
        {
            parkingSpots.Add(parkingSpot);
        }

        public ParkingSpot GetAvailableParkingSpotReserved(Business reserved)
        {
            throw new NotImplementedException();
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsByPlate()
        {
            throw new NotImplementedException();
        }

        public List<ParkingSpot>? GetAvailableParkingSpotsReserved(Business reserved)
        {
            throw new NotImplementedException();
        }

        public ParkingSpot? GetAvailableParkingSpotUnreserved()
        {
            ParkingSpot parkingspot = new ParkingSpot(employee: null, visitor: null, reserved: null, plate: null);

            parkingSpots.Add(parkingspot);

            return parkingSpots.FirstOrDefault(p => p.Reserved == null && p.Plate == null && p.IsDeleted == false);
        }

        public ParkingSpot? GetParkingSpotByEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public ParkingSpot? GetParkingSpotByPlate(string? licensePlate)
        {
            if (parkingSpots.Exists(parkingSpot => parkingSpot.Plate == licensePlate))
            {
                return parkingSpots.Find(parkingSpot => parkingSpot.Plate == licensePlate);

            }
            else
            {
                return null;
            }

        }

        public ParkingSpot? GetParkingSpotByVisitor(Visitor visitor)
        {
            return parkingSpots.Find(parkingSpot => parkingSpot.Visitor == visitor);
        }

        public List<ParkingSpot> GetParkingSpots()
        {
            return parkingSpots;
        }

        public List<ParkingSpot> GetParkingSpotsByReserved(Business reserved)
        {
            return parkingSpots.FindAll(parkingSpot => parkingSpot.Reserved == reserved);
        }

        public bool ParkingSpotExist(ParkingSpot? parkingSpot)
        {
            throw new NotImplementedException();
        }

        public bool ParkingSpotExist(string plate)
        {

            if (parkingSpots.FirstOrDefault(p => p.Plate == plate) != null) return true;
            return false;
        }

        public void UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            parkingSpots[parkingSpot.Id] = parkingSpot;
        }
    }
}