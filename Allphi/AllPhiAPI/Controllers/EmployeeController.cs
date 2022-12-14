using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace AllPhiAPI.Controllers
{
    [Route("Employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        //webapi using iemployeerepository
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // all get routes
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            if (employees.Count == 0) return NoContent();
            return Ok(employees.Select(e => new EmployeeDto(e)));
        }

        [HttpGet("id/{id}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null) return NoContent();
            return Ok(new EmployeeDto(employee));
        }

        //get by name
        [HttpGet("name/{name}", Name = "GetEmployeeByName")]
        public IActionResult GetEmployeeByName(string name)
        {
            var employee = _employeeRepository.GetEmployeeByName(name);
            if (employee == null) return NoContent();
            return Ok(new EmployeeDto(employee));
        }

        //get employees by name
        [HttpGet("employee/{employeeNames}", Name = "GetEmployeesByName")]
        public IActionResult GetEmployeesByName(string name)
        {
            var employees = _employeeRepository.GetEmployeesByName(name);
            if (employees.Count == 0) return NoContent();
            return Ok(employees.Select(e => new EmployeeDto(e)));
        }

        //get employees by business
        [HttpGet("business/{business}", Name = "GetEmployeesByBusiness")]
        public IActionResult GetEmployeesByBusiness(Business business)
        {
            var employees = _employeeRepository.GetEmployeesByBusiness(business);
            if (employees.Count == 0) return NoContent();
            return Ok(employees.Select(e => new EmployeeDto(e)));
        }

        //get employee by plate
        [HttpGet("plate/{plate}", Name = "GetEmployeeByPlate")]
        public IActionResult GetEmployeeByPlate(string plate)
        {
            var employee = _employeeRepository.GetEmployeeByPlate(plate);
            if (employee == null) return NoContent();
            return Ok(new EmployeeDto(employee));
        }

        //create employee
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            _employeeRepository.CreateEmployee(employee);
            return Ok();
        }

        //update employee
        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
            return Ok();
        }
    }
}