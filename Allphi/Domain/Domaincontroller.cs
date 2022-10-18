﻿using System.Collections;
using Ardalis.GuardClauses;
using Domain.Models;
using Domain.Services;
using System.Text.RegularExpressions;

namespace Domain
{
    public class DomainController
    {
        #region Decleration

        private IBusinessRepository _businessRepo;
        private IContractRepository _contractRepo;
        private IEmployeeRepository _employeeRepo;
        private IParkingSpotRepository _parkingRepo;
        private IVisitorRepository _visitorRepo;
        private IVisitRepository _visitRepo;

        #endregion Decleration

        #region CTOR

        public DomainController(IBusinessRepository businessRepo, IContractRepository contractRepo, IEmployeeRepository employeeRepo, IParkingSpotRepository parkingRepo, IVisitorRepository visitorRepo, IVisitRepository visitRepo)
        {
            this._businessRepo = businessRepo;
            this._contractRepo = contractRepo;
            this._employeeRepo = employeeRepo;
            this._parkingRepo = parkingRepo;
            this._visitorRepo = visitorRepo;
            this._visitRepo = visitRepo;
        }

        #endregion CTOR

        #region Validation

        public bool IsEmailValid(string emailAddress)
        {
            Regex regex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            Match match = regex.Match(emailAddress);
            return match.Success;
        }

        public bool IsBtwValid(string btwNumber)
        {
            Regex regexBE = new Regex(@"(BE)?0[0-9]{9}");
            Match match = regexBE.Match(btwNumber);
            return match.Success;
        }

        public bool IsLicensePlateValid(string licensePlate)
        {
            Regex regex = new Regex(@"^[0-9]?([A-Z]{3}[0-9]{3}|[0-9]{3}[A-Z]{3})$");
            Match match = regex.Match(licensePlate);
            return match.Success;
        }

        #endregion Validation

        #region LicensePlate

        public Employee? CheckEmployeePlate(string plate)
        {
            return _employeeRepo.GetEmployeeByPlate(plate);
        }

        #endregion LicensePlate

        #region Parking

        public ParkingSpot GetAvailableParkingSpotVisitor()
        {
            return (_parkingRepo.GetAvailableParkingSpotUnreserved());
        }

        public void SubmitVisitorParking(string licensePlate)
        {
            string license = Guard.Against.NullOrEmpty(licensePlate, nameof(licensePlate));
            ParkingSpot parkingSpot = _parkingRepo.GetAvailableParkingSpotUnreserved();
            if (IsLicensePlateValid(license) && parkingSpot != null && _parkingRepo.CountParkingSpotByPlate(license) == 0)
            {
                parkingSpot.Plate = license;
                _parkingRepo.UpdateParkingSpot(parkingSpot);
            };
        }

        public void SubmitEmployeeParking(string licensePlate)
        {
            string license = Guard.Against.NullOrEmpty(licensePlate, nameof(licensePlate));
            Employee employee = _employeeRepo.GetEmployeeByPlate(licensePlate);
            ParkingSpot parkingSpot = _parkingRepo.GetAvailableParkingSpotReserved(employee.Business);
            if (parkingSpot == null) parkingSpot = _parkingRepo.GetAvailableParkingSpotUnreserved();
            if (IsLicensePlateValid(licensePlate) && parkingSpot != null &&
                _parkingRepo.CountParkingSpotByPlate(license) == 0)
            {
                parkingSpot.Plate = license;
                _parkingRepo.UpdateParkingSpot(parkingSpot);
            }
        }

        #endregion Parking

        #region Business

        public void CreateBusiness(string name, string? address, string? phone, string email, string btw)
        {
            _businessRepo.CreateBusiness(new Business(name, btw, email, address, phone));
        }

        public List<Business> GetBusinesses()
        {
            return _businessRepo.GetBusinesses();
        }

        #endregion Business

        public List<Visitor> GetVisitors()
        {
            return _visitorRepo.GetVisitors();
        }

        public List<Contract> GetContracts()
        {
            return _contractRepo.GetContracts();
        }

        public List<Employee> GetEmployees()

        {
            return _employeeRepo.GetEmployees();
        }

        public List<string> ConvertBusinessToStringList(object input)
        {
            Business selected = (Business)input;
            List<string> business = new List<string>();
            business.Add(selected.Name);
            business.Add(selected.Address);
            business.Add(selected.Phone);
            business.Add(selected.Email);
            business.Add(selected.Btw);
            return business;
        }

        public List<string> ConvertVisitorToStringList(object input)
        {
            Visitor selected = (Visitor)input;
            List<string> visitor = new List<string>();
            visitor.Add(selected.Name);
            visitor.Add(selected.Email);
            visitor.Add(selected.Plate);
            visitor.Add(selected.Business);
            return visitor;
        }

        public void CreateVisitor(string name, string email, string? plate, string business)
        {
            _visitorRepo.CreateVisitor(new Visitor(name, email, business, plate));
        }

        public List<string> ConvertContractToStringList(object selectedItem)
        {
            Contract selected = (Contract)selectedItem;
            List<string> contract = new List<string>();
            contract.Add(selected.Business.Name);
            contract.Add(selected.TotalSpaces.ToString());
            contract.Add(selected.StartDate.ToString());
            contract.Add(selected.EndDate.ToString());
            return contract;
        }

        public void CreateContract(string spots, string business, DateTime start, DateTime end)
        {
            int totalSpots = int.Parse(spots);
            Business selectedBusiness = _businessRepo.GetBusinessByName(business);
            _contractRepo.CreateContract(new Contract(selectedBusiness, start, end, totalSpots));
        }

        public List<string> ConvertEmployeeToStringList(object selectedItem)
        {
            Employee selected = (Employee)selectedItem;
            List<string>? employee = new List<string>();
            employee.Add(selected.Name);
            employee.Add(selected.Email);
            employee.Add(selected.Business.Name);
            employee.Add(selected.Function);
            employee.Add(selected.Plate);

            return employee;
        }

        public void CreateEmployee(string name, string? email, string function, string business, string? plate)
        {
            Business selectedBusiness = _businessRepo.GetBusinessByName(business);
            List<string> names = name.Split(' ').ToList();
            _employeeRepo.CreateEmployee(new Employee(names[0], names[1], function, selectedBusiness, email, plate));
        }
    }
}