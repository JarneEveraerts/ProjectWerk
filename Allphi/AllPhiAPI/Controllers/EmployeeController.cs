using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("Employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeBusinessService _employeeBusinessService;

        public EmployeeController(IEmployeeService employeeService, IEmployeeBusinessService employeeBusinessService)
        {
            _employeeService = employeeService;
            _employeeBusinessService = employeeBusinessService;
        }
        #region GET   

        [HttpGet]
        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeService.GetEmployees();
        }

        [HttpGet("{name}/businessId")]
        public int GetBusinessIdByEmployeeName(string name)
        {
            return _employeeService.GetBusinessIdByEmployeeName(name);
        }
        //all business staan op null
        [HttpGet("business/{business}")]
        public async Task<List<Employee>> GetEmployeesByBusiness([FromRoute] string business)
        {
            return await _employeeService.GetEmployeesByBusiness(business);
        }

        [HttpGet("name/{name}")]
        public Employee GetEmployeeByName([FromRoute] string name)
        {
            return _employeeService.GetEmployeeByName(name);
        }

        [HttpGet("licenseplate/{licensePlate}")]
        public Employee GetEmployeeByPlate(string licensePlate)
        {
            return _employeeService.GetEmployeeByPlate(licensePlate);
        }
        //GetEmployeeIdByName
        #endregion

        #region CREATE
        [HttpPut]
        public void CreateEmployee(CreateAndUpdateEmployeeDTO employee)
        {
            var business = _employeeBusinessService.GetBusinessByIdForEmployee(employee.Business.Id);
            employee.Business = business;
            _employeeService.CreateEmployee(employee);
        }
        #endregion

        #region UPDATE

        [HttpPatch("{id}")]
        public void UpdateEmployee(CreateAndUpdateEmployeeDTO employee, [FromRoute] int id)
        {
            _employeeService.UpdateEmployee(employee, id);
        }

        [HttpDelete("{id}")]
        public void DeleteEmployee([FromRoute] int id)
        {
            _employeeService.DeleteEmployee(id);
        }
        #endregion
    }
}
