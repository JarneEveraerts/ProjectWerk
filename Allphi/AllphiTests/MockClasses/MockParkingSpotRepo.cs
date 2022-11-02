using Domain.Models;
using Domain.Services;

namespace AllphiTests.MockClasses
{
     class MockParkingSpotRepo : IParkingSpotRepository
    {
        public int CountParkingSpotByPlate(string plate)
        {
            throw new NotImplementedException();
        }

        public void CreateParkingSpot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
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
            return new ParkingSpot();
        }

        public ParkingSpot? GetParkingSpotByEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public ParkingSpot? GetParkingSpotByPlate(string? licensePlate)
        {
            throw new NotImplementedException();
        }

        public ParkingSpot? GetParkingSpotByVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public List<ParkingSpot> GetParkingSpots()
        {
            throw new NotImplementedException();
        }

        public List<ParkingSpot> GetParkingSpotsByReserved(Business reserved)
        {
            throw new NotImplementedException();
        }

        public void UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }
    }
}