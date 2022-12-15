
﻿using System.Collections;
using System.Net.Mail;
using Ardalis.GuardClauses;
using Domain.Models;
using Domain.Services;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Padi.Vies;

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

        public DomainController(IBusinessRepository businessRepo, IContractRepository contractRepo,
            IEmployeeRepository employeeRepo, IParkingSpotRepository parkingRepo, IVisitorRepository visitorRepo,
            IVisitRepository visitRepo)
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

        public bool IsEmailValid(string email)
        {
            try
            {
                MailAddress Email = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsBtwValid(string btwNumber)
        {
            var result = ViesManager.IsValid(btwNumber);
            return result.IsValid;
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
            if (IsLicensePlateValid(license) && parkingSpot != null &&
                _parkingRepo.CountParkingSpotByPlate(license) == 0)
            {
                parkingSpot.Plate = license;
                _parkingRepo.UpdateParkingSpot(parkingSpot);
            }
        }

        public bool ExitParking(string input)
        {
            ParkingSpot parkingSpot = _parkingRepo.GetParkingSpotByPlate(input);
            if (parkingSpot != null)
            {
                parkingSpot.IsDeleted = true;
                _parkingRepo.UpdateParkingSpot(parkingSpot);
                return true;
            }
            return false;
        }

        public bool EnterParking(string licensePlate, string name)
        {
            Business business = _businessRepo.GetBusinessByName(name);
            Contract? contract = _contractRepo.GetContractByBusiness(business);
            Employee? employee = _employeeRepo.GetEmployeeByPlate(licensePlate);
            if (_parkingRepo.GetParkingSpotByPlate(licensePlate) != null) return false;
            if (contract != null && contract.TotalSpaces >= _parkingRepo.GetParkingSpotsByReserved(business).Count)
            {
                _parkingRepo.CreateParkingSpot(new ParkingSpot(employee, null, licensePlate, business));
                return true;
            }
            _parkingRepo.CreateParkingSpot(new ParkingSpot(employee, null, licensePlate, null));
            return true;
        }

        public bool ParkingSpotExists(string visitorPlate)
        {
            return _parkingRepo.ParkingSpotExist(visitorPlate);
        }

        #endregion Parking

        #region GET

        public List<Business> GetBusinesses()
        {
            return _businessRepo.GetBusinesses();
        }

        public Business GetBusinessById(int id)
        {
            return _businessRepo.GetBusinessById(id);
        }

        public List<Visitor> GetVisitors()
        {
            return _visitorRepo.GetVisitors();
        }

        private Visitor GetVisitorById(int id)
        {
            return _visitorRepo.GetVisitorById(id);
        }

        public List<Contract> GetContracts()
        {
            return _contractRepo.GetContracts();
        }

        public List<Employee> GetEmployees()

        {
            return _employeeRepo.GetEmployees();
        }

        public List<ParkingSpot> GetParkingSpots()
        {
            return _parkingRepo.GetParkingSpots();
        }

        public List<List<string>> GiveBusinesses()
        {
            return GetBusinesses().Select(business => new List<string>() { business.Name, business.Btw, business.Email, business.Address, business.Phone, business.IsDeleted.ToString() }).ToList();
        }

        public List<Visit> GetVisits()
        {
            return _visitRepo.GetVisits();
        }

        public int GetEmployeeIdByName(string name)
        {
            Employee employee = _employeeRepo.GetEmployeeByName(name);
            return employee.Business.Id;
        }

        public Business GetBusinessIdByEmployeeName(string name)
        {
            return _businessRepo.GetBusinessById(GetEmployeeIdByName(name));
        }

        public List<Employee> GetEmployeesByBusiness(string business)
        {
            return _employeeRepo.GetEmployeesByBusiness(GetBusinessByName(business));
        }

        private Business GetBusinessByName(string business)
        {
            return _businessRepo.GetBusinessByName(business);
        }

        public Visitor GetVisitorByEmail(string visitorEmail)
        {
            return _visitorRepo.GetVisitorByMail(visitorEmail);
        }

        public Visitor GetVisitorByName(string name)
        {
            return _visitorRepo.GetVisitorByName(name);
        }

        public Visit GetVisitByName(string name)
        {
            return _visitRepo.GetVisitByVisitor(GetVisitorByName(name));
        }

        public Employee GetEmployeeByName(string name)
        {
            return _employeeRepo.GetEmployeeByName(name);
        }

        public Business GetBusinessByBtw(string btw)
        {
            return _businessRepo.GetBusinessByBTW(btw);
        }

        public Contract GetContractByBusiness(string business)
        {
            return _contractRepo.GetContractByBusiness(GetBusinessByName(business));
        }

        #endregion GET

        #region UPDATE

        public void UpdateEmployee(string name, string email, string function, string business, string plate, int id)
        {
            Employee employee = _employeeRepo.GetEmployeeById(id);
            employee.Name = name;
            employee.Email = email;
            employee.Function = function;
            employee.Business = _businessRepo.GetBusinessByName(business);
            employee.Plate = plate;
            _employeeRepo.UpdateEmployee(employee);
        }

        public void UpdateContract(string spots, string business, DateTime start, DateTime end, int id)
        {
            Contract contract = _contractRepo.GetContractById(id);
            contract.Business = _businessRepo.GetBusinessByName(business);
            contract.TotalSpaces = Convert.ToInt16(spots);
            contract.StartDate = start;
            contract.EndDate = end;
            _contractRepo.UpdateContract(contract);
        }

        public void UpdateVisitor(string name, string email, string plate, string business, int id)
        {
            Visitor visitor = _visitorRepo.GetVisitorById(id);
            visitor.Name = name;
            visitor.Email = email;
            visitor.Plate = plate;
            visitor.Business = business;
            _visitorRepo.UpdateVisitor(visitor);
        }

        public void UpdateBusiness(string name, string address, string phone, string email, string btw, int id)
        {
            Business business = _businessRepo.GetBusinessById(id);
            business.Name = name;
            business.Address = address;
            business.Phone = phone;
            business.Email = email;
            business.Btw = btw;
            _businessRepo.UpdateBusiness(business);
        }

        public void UpdateVisit(string name, string employee, string business, DateTime start, DateTime end)
        {
            Visit visit = _visitRepo.GetVisitByVisitor(GetVisitorByName(name));
            visit.Visitor = GetVisitorByName(name);
            visit.Employee = _employeeRepo.GetEmployeeByName(employee);
            visit.Business = _businessRepo.GetBusinessByName(business);
            visit.StartDate = start;
            visit.EndDate = end;
            _visitRepo.UpdateVisit(visit);
        }

        #endregion UPDATE

        #region DELETE

        public void DeleteVisitor(int id)
        {
            Visitor visitor = _visitorRepo.GetVisitorById(id);
            visitor.IsDeleted = true;
            _visitorRepo.UpdateVisitor(visitor);
        }

        public void DeleteContract(int id)
        {
            Contract contract = _contractRepo.GetContractById(id);
            contract.IsDeleted = true;
            _contractRepo.UpdateContract(contract);
        }

        public void DeleteBusiness(int id)
        {
            Business business = _businessRepo.GetBusinessById(id);
            business.IsDeleted = true;
            _businessRepo.UpdateBusiness(business);
        }

        public void DeleteEmployee(int id)
        {
            Employee employee = _employeeRepo.GetEmployeeById(id);
            employee.IsDeleted = true;
            _employeeRepo.UpdateEmployee(employee);
        }

        public void DeleteVisit(string name)
        {
            Visit visit = _visitRepo.GetVisitByVisitor(GetVisitorByName(name));
            visit.IsDeleted = true;
            _visitRepo.UpdateVisit(visit);
        }

        #endregion DELETE

        #region CREATE

        public void CreateEmployee(string name, string? email, string function, string business, string? plate)
        {
            Business selectedBusiness = _businessRepo.GetBusinessByName(business);
            List<string> names = name.Split(' ').ToList();
            _employeeRepo.CreateEmployee(new Employee(names[0], names[1], function, selectedBusiness, email, plate));
        }

        public void CreateVisitor(string name, string email, string organisation, string employee, string business)
        {
            Visitor visitor = new Visitor(name, email, organisation, null);
            _visitorRepo.CreateVisitor(visitor);
            CreateVisit(visitor, employee, business);
        }

        public void CreateContract(string spots, string business, DateTime start, DateTime end)
        {
            int totalSpots = int.Parse(spots);
            Business selectedBusiness = _businessRepo.GetBusinessByName(business);
            _contractRepo.CreateContract(new Contract(selectedBusiness, start, end, totalSpots));
        }

        public void CreateBusiness(string name, string address, string phone, string email, string btw)
        {
            _businessRepo.CreateBusiness(new Business(name, btw, email, address, phone));
        }

        public void CreateVisit(Visitor visitor, string employee, string business)
        {
            Visit visit = new();
            visit.Visitor = visitor;
            visit.StartDate = DateTime.Now;
            visit.Business = _businessRepo.GetBusinessByName(business);
            visit.Employee = _employeeRepo.GetEmployeeByName(employee);
            _visitRepo.CreateVisit(visit);
        }

        public void CreateVisitorWithPlate(string visitorName, string visitorEmail, string visitorPlate, string organisation, string employee, string business)
        {
            ParkingSpot? parkingSpot = _parkingRepo.GetParkingSpotByPlate(visitorPlate);
            Visitor? visitor = new Visitor(visitorName, visitorEmail, organisation, visitorPlate);
            if (ParkingSpotExists(visitorPlate))
            {
                _visitorRepo.CreateVisitor(visitor);
                parkingSpot.Visitor = visitor;
                _parkingRepo.UpdateParkingSpot(parkingSpot);
                CreateVisit(visitor, employee, business);
            }
        }

        public void CreateVisitorBalie(string name, string email, string plate, string business)
        {
            _visitorRepo.CreateVisitor(new Visitor(name, email, business, plate));
        }

        #endregion CREATE
    }
﻿using System.Collections;
using System.Net.Mail;
using Ardalis.GuardClauses;
using Domain.Models;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Padi.Vies;
using Domain.Models.DTOs;
using System.Xml.Linq;
using Domain.Repositories;

namespace Domain
{
    public class DomainController : IDomainController
    {
        #region Validation

        public bool IsEmailValid(string email)
        {
            try
            {
                MailAddress Email = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsBtwValid(string btwNumber)
        {
            var result = ViesManager.IsValid(btwNumber);
            return result.IsValid;
        }

        public bool IsLicensePlateValid(string licensePlate)
        {
            Regex regex = new Regex(@"^[0-9]?([A-Z]{3}[0-9]{3}|[0-9]{3}[A-Z]{3})$");
            Match match = regex.Match(licensePlate);
            return match.Success;
        }

        #endregion Validation

    }
}