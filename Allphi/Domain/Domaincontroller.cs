using System.Collections;
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
            if (IsLicensePlateValid(license) && parkingSpot != null &&
                _parkingRepo.CountParkingSpotByPlate(license) == 0)
            {
                parkingSpot.Plate = license;
                _parkingRepo.UpdateParkingSpot(parkingSpot);
            }

            ;
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

        #region GET

        public List<Business> GetBusinesses()
        {
            return _businessRepo.GetBusinesses();
        }

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

        #endregion DELETE

        #region CREATE

        public void CreateEmployee(string name, string? email, string function, string business, string? plate)
        {
            Business selectedBusiness = _businessRepo.GetBusinessByName(business);
            List<string> names = name.Split(' ').ToList();
            _employeeRepo.CreateEmployee(new Employee(names[0], names[1], function, selectedBusiness, email, plate));
        }

        public void CreateVisitor(string name, string email, string? plate, string business)
        {
            _visitorRepo.CreateVisitor(new Visitor(name, email, business, plate));
        }

        public void CreateContract(string spots, string business, DateTime start, DateTime end)
        {
            int totalSpots = int.Parse(spots);
            Business selectedBusiness = _businessRepo.GetBusinessByName(business);
            _contractRepo.CreateContract(new Contract(selectedBusiness, start, end, totalSpots));
        }

        public void CreateBusiness(string name, string address, string phone, string email, string btw)
        {
            _businessRepo.CreateBusiness(new Business(name, address, phone, email, btw));
        }

        #endregion CREATE
    }
}