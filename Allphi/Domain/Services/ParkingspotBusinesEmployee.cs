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
    public class ParkingspotBusinesEmployeeService : IParkingspotBusinesEmployeeService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IParkingSpotRepository _parkingSpotRepository;

        public ParkingspotBusinesEmployeeService(IBusinessRepository businessRepository, IEmployeeRepository employeeRepository, IParkingSpotRepository parkingSpotRepository)
        {
            this._parkingSpotRepository = parkingSpotRepository;
            this._businessRepository = businessRepository;
            this._employeeRepository = employeeRepository;
        }

        public bool EnterParking(EnterParkingDTO enterParking)
        {
            var business = _businessRepository.GetBusinessById(enterParking.Business.Id);
            enterParking.Business = business;
            var employee = _employeeRepository.GetEmployeeById(enterParking.Employee.Id);
            enterParking.Employee = employee;
            _parkingSpotRepository.CreateParkingSpot(new ParkingSpot(
                enterParking.Employee,
                enterParking.Visitor,
                enterParking.Plate,
                enterParking.Business


           ));
            return true;
        }
    }
}
