using Ardalis.GuardClauses;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IDomainController _domainController;

        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository, IDomainController domainController)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _domainController = domainController;
        }
        public ParkingSpot GetAvailableParkingSpotVisitor()
        {
            return _parkingSpotRepository.GetAvailableParkingSpotUnreserved();
        }

        public void SubmitVisitorParking(string licensePlate)
        {
            string license = Guard.Against.NullOrEmpty(licensePlate, nameof(licensePlate));
            ParkingSpot parkingSpot = _parkingSpotRepository.GetAvailableParkingSpotUnreserved();
            
            if (_domainController.IsLicensePlateValid(license) && parkingSpot != null &&
                _parkingSpotRepository.CountParkingSpotByPlate(license) == 0)
            {
                parkingSpot.Plate = license;
                _parkingSpotRepository.UpdateParkingSpot(parkingSpot);
            }
        }

        public bool ExitParking(string input)
        {
            ParkingSpot parkingSpot = _parkingSpotRepository.GetParkingSpotByPlate(input);
            if (parkingSpot != null)
            {
                parkingSpot.IsDeleted = true;
                _parkingSpotRepository.UpdateParkingSpot(parkingSpot);
                return true;
            }
            return false;
        }

        public bool EnterParking(EnterParkingDTO enterParkingDTO)
        {
            if (_parkingSpotRepository.GetParkingSpotByPlate(enterParkingDTO.Plate) != null) return false;
            if (enterParkingDTO.Contract != null && enterParkingDTO.Contract.TotalSpaces <= _parkingSpotRepository.GetParkingSpotsByReserved(enterParkingDTO.Business).Count)
            {
                _parkingSpotRepository.CreateParkingSpot(new ParkingSpot
                {
                    Plate = enterParkingDTO.Plate,
                    Reserved = enterParkingDTO.Business,
                    Employee = enterParkingDTO.Employee
                });
                return true;
            }
            _parkingSpotRepository.CreateParkingSpot(new ParkingSpot
            {
                Plate = enterParkingDTO.Plate,
                Employee = enterParkingDTO.Employee
            });
            return true;
        }

        public bool ParkingSpotExists(string visitorPlate)
        {
            return _parkingSpotRepository.ParkingSpotExist(visitorPlate);
        }
        public async Task<List<ParkingSpot>> GetParkingSpots()
        {
            return await _parkingSpotRepository.GetParkingSpots();
        }

    }
}
