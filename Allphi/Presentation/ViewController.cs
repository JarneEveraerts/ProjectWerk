using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Models;
using Newtonsoft.Json;
using Padi.Vies;
using Presentation.ViewModels;
using Shared.Dto;

namespace Presentation
{
    public class ViewController
    {
        private readonly HttpClient _api;

        public ViewController(IHttpClientFactory clientFactory)
        {
            _api = clientFactory.CreateClient();
            _api.BaseAddress = new Uri("http://localhost:5038");
        }

        #region GetViews

        public List<BusinessView> GetBusinessViews()
        {
            var response = _api.GetAsync("/Businesses").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var businesses = JsonConvert.DeserializeObject<List<BusinessDto>>(content);
            var businessViews = new List<BusinessView>();
            if (businesses != null)
            {
                foreach (var item in businesses)
                {
                    businessViews.Add(new BusinessView(item));
                }
            }
            return businessViews;
        }

        public List<EmployeeView> GetEmployeeViews()
        {
            var response = _api.GetAsync("/Employees").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(content);
            var employeeViews = new List<EmployeeView>();
            if (employees != null)
            {
                foreach (var item in employees)
                {
                    employeeViews.Add(new EmployeeView(item));
                }
            }
            return employeeViews;
        }

        public List<VisitorView> GetVisitorViews()
        {
            var response = _api.GetAsync("/Visitors").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visitors = JsonConvert.DeserializeObject<List<VisitorDto>>(content);
            var visitorViews = new List<VisitorView>();
            if (visitors != null)
            {
                foreach (var item in visitors)
                {
                    visitorViews.Add(new VisitorView(item));
                }
            }
            return visitorViews;
        }

        public List<VisitView> GetVisitViews()
        {
            var response = _api.GetAsync("/Visits").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visits = JsonConvert.DeserializeObject<List<VisitDto>>(content);
            var visitViews = new List<VisitView>();
            if (visits != null)
            {
                foreach (var item in visits)
                {
                    visitViews.Add(new VisitView(item));
                }
            }
            return visitViews;
        }

        public List<ParkingSpotView> GetParkingSpotViews()
        {
            var response = _api.GetAsync("/ParkingSpots").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var parkingSpots = JsonConvert.DeserializeObject<List<ParkingSpotDto>>(content);
            var parkingSpotViews = new List<ParkingSpotView>();
            if (parkingSpots != null)
            {
                foreach (var item in parkingSpots)
                {
                    parkingSpotViews.Add(new ParkingSpotView(item));
                }
            }
            return parkingSpotViews;
        }

        public List<ContractView> GetContractViews()
        {
            var response = _api.GetAsync("/Contracts").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var contracts = JsonConvert.DeserializeObject<List<ContractDto>>(content);
            var contractViews = new List<ContractView>();
            if (contracts != null)
            {
                foreach (var item in contracts)
                {
                    contractViews.Add(new ContractView(item));
                }
            }
            return contractViews;
        }

        #endregion GetViews

        #region GetBusiness

        // all get methodes for business
        public BusinessView GetBusinessById(int id)
        {
            var response = _api.GetAsync($"/Businesses/{id}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var business = JsonConvert.DeserializeObject<BusinessDto>(content);
            if (business == null) return null;
            return new BusinessView(business);
        }

        public BusinessView GetBusinessByBtw(string btw)
        {
            var response = _api.GetAsync($"/Businesses/btw/{btw}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var business = JsonConvert.DeserializeObject<BusinessDto>(content);
            if (business == null) return null;
            return new BusinessView(business);
        }

        public BusinessView GetBusinessByName(string name)
        {
            var response = _api.GetAsync($"/Businesses/name/{name}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var business = JsonConvert.DeserializeObject<BusinessDto>(content);
            if (business == null) return null;
            return new BusinessView(business);
        }

        public BusinessView GetBusinessByEmployeeName(string employee)
        {
            var response = _api.GetAsync($"/Employees/name/{employee}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(content);
            if (employeeDto == null) return null;
            return new BusinessView(employeeDto.Business);
        }

        #endregion GetBusiness

        #region Create

        public void CreateBusiness(string name, string address, string phone, string email, string btw)
        {
            var business = new BusinessDto
            {
                Name = name,
                Address = address,
                Phone = phone,
                Email = email,
                Btw = btw
            };
            var json = JsonConvert.SerializeObject(business);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Businesses", content);
        }

        #endregion Create

        public VisitorView GetVisitorByName(string name)
        {
            var response = _api.GetAsync($"/Visitors/name/{name}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visitor = JsonConvert.DeserializeObject<VisitorDto>(content);
            if (visitor == null) return null;
            return new VisitorView(visitor);
        }

        public ContractView GetContractByBusiness(string business)
        {
            var response = _api.GetAsync($"/Contracts/business/{GetBusinessByName(business)}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var contract = JsonConvert.DeserializeObject<ContractDto>(content);
            if (contract == null) return null;
            return new ContractView(contract);
        }

        public void CreateContract(int spots, string business, DateTime start, DateTime end)
        {
            var contract = new ContractDto
            {
                TotalSpaces = spots,
                Reserved = GetBusinessDto(business),
                StartDate = start,
                EndDate = end
            };
            var json = JsonConvert.SerializeObject(contract);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Contracts", content);
        }

        public EmployeeView GetEmployeeByName(string name)
        {
            var response = _api.GetAsync($"/Employees/name/{name}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(content);
            if (employee == null) return null;
            return new EmployeeView(employee);
        }

        public List<EmployeeView> GetEmployeesByBusiness(string business)
        {
            BusinessView _business = GetBusinessByName(business);
            var response = _api.GetAsync($"/Employees/business/{_business}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(content);
            List<EmployeeView> employeeViews = new();
            if (employees != null)
            {
                foreach (var item in employees)
                {
                    employeeViews.Add(new EmployeeView(item));
                }
            }
            return employeeViews;
        }

        public void UpdateBusiness(string name, string address, string phone, string email, string btw, int businessViewId)
        {
            var business = new BusinessDto
            {
                Name = name,
                Address = address,
                Phone = phone,
                Email = email,
                Btw = btw
            };
            var json = JsonConvert.SerializeObject(business);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Businesses", content);
        }

        public void UpdateVisitor(string name, string email, string plate, string business, int visitorViewId)
        {
            var visitor = new VisitorDto
            {
                Name = name,
                Email = email,
                Plate = plate,
                Business = business
            };
            var json = JsonConvert.SerializeObject(visitor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Visitors", content);
        }

        public void UpdateContract(string spots, string business, DateTime start, DateTime end, int contractViewId)
        {
            var contract = new ContractDto
            {
                TotalSpaces = int.Parse(spots),
                Reserved = GetBusinessDto(business),
                StartDate = start,
                EndDate = end
            };

            var json = JsonConvert.SerializeObject(contract);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Contracts", content);
        }

        private BusinessDto GetBusinessDto(string business)
        {
            var response = _api.GetAsync($"/Businesses/name/{business}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var businessDto = JsonConvert.DeserializeObject<BusinessDto>(content);
            return businessDto;
        }

        public void UpdateEmployee(string name, string email, string function, string business, string plate, int employeeViewId)
        {
            var employee = new EmployeeDto
            {
                Name = name,
                Email = email,
                Function = function,
                Business = GetBusinessDto(business),
                Plate = plate
            };
            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Employees", content);
        }

        public void UpdateVisit(string name, string employee, string business, DateTime start, DateTime end)
        {
            var visit = new VisitDto
            {
                Visitor = GetVisitorDto(name),
                Employee = GetEmployeeDto(employee),
                Business = GetBusinessDto(business),
                StartDate = start,
                EndDate = end
            };
            var json = JsonConvert.SerializeObject(visit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Visits", content);
        }

        private EmployeeDto GetEmployeeDto(string employee)
        {
            var response = _api.GetAsync($"/Employees/name/{employee}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(content);
            return employeeDto;
        }

        private VisitorDto GetVisitorDto(string name)
        {
            var response = _api.GetAsync($"/Visitors/name/{name}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visitorDto = JsonConvert.DeserializeObject<VisitorDto>(content);
            return visitorDto;
        }

        public void CreateEmployee(string name, string email, string function, string business, string plate)
        {
            var employee = new EmployeeDto
            {
                Name = name,
                Email = email,
                Function = function,
                Business = GetBusinessDto(business),
                Plate = plate
            };
            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Employees", content);
        }

        public VisitView GetVisitByName(string name)
        {
            var response = _api.GetAsync($"/Visits/name/{name}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visit = JsonConvert.DeserializeObject<VisitDto>(content);
            if (visit == null) return null;
            return new VisitView(visit);
        }

        public void CreateVisit(string visitor, string employee, string business)
        {
            var visit = new VisitDto
            {
                Visitor = GetVisitorDto(visitor),
                Employee = GetEmployeeDto(employee),
                Business = GetBusinessDto(business),
                StartDate = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(visit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Visits", content);
        }

        public void DeleteVisitor(int visitorViewId)
        {
            VisitorView visitor = GetVisitorById(visitorViewId);
            visitor.IsDeleted = true;
            var json = JsonConvert.SerializeObject(visitor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Visitors", content);
        }

        private VisitorView GetVisitorById(int visitorViewId)
        {
            var response = _api.GetAsync($"/Visitors/id/{visitorViewId}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visitor = JsonConvert.DeserializeObject<VisitorDto>(content);
            if (visitor == null) return null;
            return new VisitorView(visitor);
        }

        public void DeleteContract(int contractViewId)
        {
            ContractView contract = GetContractById(contractViewId);
            contract.IsDeleted = true;
            var json = JsonConvert.SerializeObject(contract);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Contracts", content);
        }

        private ContractView GetContractById(int contractViewId)
        {
            var response = _api.GetAsync($"/Contracts/id/{contractViewId}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var contract = JsonConvert.DeserializeObject<ContractDto>(content);
            if (contract == null) return null;
            return new ContractView(contract);
        }

        public void DeleteBusiness(int businessViewId)
        {
            BusinessView business = GetBusinessById(businessViewId);
            business.IsDeleted = true;
            var json = JsonConvert.SerializeObject(business);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Businesses", content);
        }

        public void DeleteEmployee(int employeeViewId)
        {
            EmployeeView employee = GetEmployeeById(employeeViewId);
            employee.IsDeleted = true;
            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Employees", content);
        }

        private EmployeeView GetEmployeeById(int employeeViewId)
        {
            var response = _api.GetAsync($"/Employees/id/{employeeViewId}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(content);
            if (employee == null) return null;
            return new EmployeeView(employee);
        }

        public void DeleteVisit(string name)
        {
            VisitView visit = GetVisitByName(name);
            visit.IsDeleted = true;
            var json = JsonConvert.SerializeObject(visit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/Visits", content);
        }

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

        public bool EnterParking(string licensePlate, string businessName)
        {
            ContractView? contract = GetContractByBusiness(businessName);
            EmployeeView? employee = GetEmployeeByPlate(licensePlate);
            if (GetParkingSpotByPlate(licensePlate) != null) return false;
            if (contract != null && contract.TotalSpaces <= GetParkingSpotsByReserved(businessName).Count)
            {
                CreateParkingSpot(employee, licensePlate, businessName);
                return true;
            }
            CreateParkingSpot(employee, licensePlate, null);
            return true;
        }

        public void CreateParkingSpot(EmployeeView? employee, string licensePlate, string businessName)
        {
            var parkingSpot = new ParkingSpotDto
            {
                Employee = employee != null ? GetEmployeeDto(employee.Name) : null,
                Visitor = null,
                Plate = licensePlate,
                ReservedId = GetBusinessDto(businessName).Id,
                IsDeleted = false
            };
            var json = JsonConvert.SerializeObject(parkingSpot);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/ParkingSpots", content);
        }

        public void CreateVisitorBalie(string name, string email, string plate, string business)
        {
            var visitor = new VisitorDto
            {
                Name = name,
                Email = email,
                Plate = plate,
                Business = business
            };
            var json = JsonConvert.SerializeObject(visitor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Visitors", content);
        }

        private List<ParkingSpotView> GetParkingSpotsByReserved(string business)
        {
            var response = _api.GetAsync($"/ParkingSpots/reserved/{business}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var parkingSpots = JsonConvert.DeserializeObject<List<ParkingSpotDto>>(content);
            if (parkingSpots.Count == 0) return null;
            return parkingSpots.Select(parkingSpot => new ParkingSpotView(parkingSpot)).ToList();
        }

        private EmployeeView? GetEmployeeByPlate(string licensePlate)
        {
            var response = _api.GetAsync($"/Employees/plate/{licensePlate}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(content);
            if (employee == null) return null;
            return new EmployeeView(employee);
        }

        private ParkingSpotView? GetParkingSpotByPlate(string licensePlate)
        {
            var response = _api.GetAsync($"/ParkingSpots/plate/{licensePlate}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var parkingSpot = JsonConvert.DeserializeObject<ParkingSpotDto>(content);
            if (parkingSpot == null) return null;
            return new ParkingSpotView(parkingSpot);
        }

        public bool ExitParking(string input)
        {
            ParkingSpotView parkingSpot = GetParkingSpotByPlate(input);
            if (parkingSpot == null) return false;
            parkingSpot.IsDeleted = true;
            var json = JsonConvert.SerializeObject(parkingSpot);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/ParkingSpots", content);
            return true;
        }

        public void CreateVisitor(string visitorName, string visitorEmail, string organisation, string? employeeName, string? businessName)
        {
            var visitor = new VisitorDto
            {
                Name = visitorName,
                Email = visitorEmail,
                Plate = null,
                Business = organisation
            };
            var json = JsonConvert.SerializeObject(visitor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Visitors", content);
            CreateVisit(visitorName, employeeName, businessName);
        }

        public void CreateVisitorWithPlate(string visitorName, string visitorEmail, string visitorPlate, string organisation, string? employeeName, string? businessName)
        {
            ParkingSpotView spot = GetParkingSpotByPlate(visitorPlate);
            var visitor = new VisitorDto
            {
                Name = visitorName,
                Email = visitorEmail,
                Plate = visitorPlate,
                Business = organisation
            };
            var json = JsonConvert.SerializeObject(visitor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PostAsync("/Visitors", content);
            spot.Visitor = visitor;
            updateParkingSpot(spot);
            CreateVisit(visitorName, employeeName, businessName);
        }

        private void updateParkingSpot(ParkingSpotView spot)
        {
            var json = JsonConvert.SerializeObject(spot);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _api.PutAsync($"/ParkingSpots", content);
        }

        public VisitorView GetVisitorByEmail(string visitorEmail)
        {
            var response = _api.GetAsync($"/Visitors/email/{visitorEmail}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var visitor = JsonConvert.DeserializeObject<VisitorDto>(content);
            if (visitor == null) return null;
            return new VisitorView(visitor);
        }

        public bool ParkingSpotExists(string visitorPlate)
        {
            var response = _api.GetAsync($"/ParkingSpots/plate/{visitorPlate}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var parkingSpot = JsonConvert.DeserializeObject<ParkingSpotDto>(content);
            if (parkingSpot == null) return false;
            return true;
        }

        public BusinessView GetBusinessIdByEmployeeName(string employeeName)
        {
            var response = _api.GetAsync($"/Businesses/employee/{employeeName}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var business = JsonConvert.DeserializeObject<BusinessDto>(content);
            if (business == null) return null;
            return new BusinessView(business);
        }
    }
}