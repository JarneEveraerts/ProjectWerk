using Domain.Models;
using Domain.Repositories;

namespace AllphiTests.MockClasses
{
    internal class MockEmployeeRepo : IEmployeeRepository
    {

        List<Employee> employeeslist = new List<Employee>();
        public void CreateEmployee(Employee employee)
        {
            employeeslist.Add(employee);
        }

        public Employee? GetEmployeeById(int id)
        {
                return employeeslist[id];
        }

        public Employee GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public Employee? GetEmployeeByPlate(string licensePlate)
        {
            List<string> licenseplatelist = new List<string>();
            foreach (Employee item in employeeslist)
            {
                licenseplatelist.Add(item.Plate);
            }
            if (licenseplatelist.Contains(licensePlate))
            {
                return employeeslist[licenseplatelist.IndexOf(licensePlate)];
            }
            else
            {
                return null;

            }
           
            
        }

        public List<Employee>? GetEmployees()
        {
            return employeeslist;
        }

        public List<Employee>? GetEmployeesByBusiness(Business business)
        {
            throw new NotImplementedException();
        }

        public List<Employee>? GetEmployeesByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            employeeslist[employee.Id] = employee;
        }
    }
}