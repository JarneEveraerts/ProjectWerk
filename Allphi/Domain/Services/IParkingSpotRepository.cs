﻿using Domain.Models;

namespace Domain.Services
{
    public interface IParkingSpotRepository
    {
        #region GET

        List<ParkingSpot> GetParkingSpots();

        List<ParkingSpot> GetParkingSpotsByReserved(Business reserved);

        List<ParkingSpot>? GetAvailableParkingSpotsByReserved(Business reserved);

        List<ParkingSpot>? GetAvailableParkingSpotsByPlate();

        ParkingSpot? GetParkingSpotByPlate(string? licensePlate);

        ParkingSpot? GetParkingSpotByEmployee(Employee employee);

        ParkingSpot? GetParkingSpotByVisitor(Visitor visitor);

        #endregion GET

        #region CREATE

        void CreateParkingSpot(ParkingSpot parkingSpot);

        #endregion CREATE

        #region UPDATE

        void UpdateParkingSpot(ParkingSpot parkingSpot);

        #endregion UPDATE

        #region DELETE

        void DeleteParkingSpot(ParkingSpot parkingSpot);

        #endregion DELETE
    }
}