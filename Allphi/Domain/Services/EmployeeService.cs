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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }
        public Task<List<Employee>> GetEmployees()

        {
            return _employeeRepo.GetEmployees();
        }
        public int GetEmployeeIdByName(string name)
        {
            Employee employee = _employeeRepo.GetEmployeeByName(name);
            return employee.Business.Id;
        }
        public async Task<List<Employee>> GetEmployeesByBusiness(string businessName)
        {
            return await _employeeRepo.GetEmployeesByBusiness(businessName);
        }
        public Employee GetEmployeeByName(string name)
        {
            return _employeeRepo.GetEmployeeByName(name);
        }
        public void UpdateEmployee(CreateAndUpdateEmployeeDTO createAndUpdateEmployeeDTO, int id)
        {
            // Moet apart gedaan worden adhv aparte httpcall van front end naar BusinessController
            // Business selectedBusiness = _businessRepo.GetBusinessByName(business);

            Employee employee = _employeeRepo.GetEmployeeById(id);
            employee.Name = createAndUpdateEmployeeDTO.Name;
            employee.Email = createAndUpdateEmployeeDTO.Email;
            employee.Function = createAndUpdateEmployeeDTO.Function;
            employee.Business = createAndUpdateEmployeeDTO.Business;
            employee.Plate = createAndUpdateEmployeeDTO.Plate;
            _employeeRepo.UpdateEmployee(employee);
        }

        public void CreateEmployee(CreateAndUpdateEmployeeDTO createAndUpdateEmployeeDTO)
        {
            List<string> names = createAndUpdateEmployeeDTO.Name.Split(' ').ToList();
            Employee employee = new Employee()
            {
                Name = names.First(),
                FirstName = names.Last(),
                Email = createAndUpdateEmployeeDTO.Email,
                Function = createAndUpdateEmployeeDTO.Function,
                Business = createAndUpdateEmployeeDTO.Business,
                Plate = createAndUpdateEmployeeDTO.Plate
            };
            _employeeRepo.CreateEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            Employee employee = _employeeRepo.GetEmployeeById(id);
            employee.IsDeleted = true;
            _employeeRepo.UpdateEmployee(employee);
        }

        public Employee GetEmployeeByPlate(string licenseplate)
        {
            return _employeeRepo.GetEmployeeByPlate(licenseplate);
        }

        public int GetBusinessIdByEmployeeName(string name)
        {
            var employee = GetEmployeeByName(name);
            return employee.Business.Id;
        }
    }
}
