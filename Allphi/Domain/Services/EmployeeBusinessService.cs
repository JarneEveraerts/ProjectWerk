using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmployeeBusinessService : IEmployeeBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeBusinessService(IBusinessRepository businessRepository, IEmployeeRepository employeeRepository)
        {
            _businessRepository = businessRepository;
            _employeeRepository = employeeRepository;
        }

        public Business GetBusinessByIdForEmployee(int id)
        {
            return _businessRepository.GetBusinessById(id);
        }
    }
}
