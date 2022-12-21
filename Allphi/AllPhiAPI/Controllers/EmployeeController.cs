using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(employees);
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return Ok(employee);
        }

        //get by name
        [HttpGet("{name}", Name = "GetEmployeeByName")]
        public IActionResult GetEmployeeByName(string name)
        {
            var employee = _employeeRepository.GetEmployeeByName(name);
            return Ok(employee);
        }

        //get employees by name
        [HttpGet("{employeenames}", Name = "GetEmployeesByName")]
        public IActionResult GetEmployeesByName(string name)
        {
            var employees = _employeeRepository.GetEmployeesByName(name);
            return Ok(employees);
        }

        //get employees by business
        [HttpGet("{business}", Name = "GetEmployeesByBusiness")]
        public IActionResult GetEmployeesByBusiness(Business business)
        {
            var employee = _employeeRepository.GetEmployeesByBusiness(business);
            return Ok(employee);
        }

        //get employee by plate
        [HttpGet("{plate}", Name = "GetEmployeeByPlate")]
        public IActionResult GetEmployeeByPlate(string plate)
        {
            var employee = _employeeRepository.GetEmployeeByPlate(plate);
            return Ok(employee);
        }

        //create employee
        [HttpPost("{Employee}", Name = "CreateEmployee")]
        public IActionResult CreateEmployee(Employee employee)
        {
            _employeeRepository.CreateEmployee(employee);
            return Ok();
        }

        //update employee
        [HttpPut("{Employee}", Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
            return Ok();
        }

    }
}
