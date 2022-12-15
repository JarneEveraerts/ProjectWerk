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
            return employeeslist.Find(employee => employee.Name == name);
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
            return employeeslist.FindAll(employee => employee.Business == business);
        }

        public List<Employee>? GetEmployeesByName(string name)
        {
            return employeeslist.FindAll(employee => employee.Name == name);
        }

        public void UpdateEmployee(Employee employee)
        {
            employeeslist[employee.Id] = employee;
        }
    }
}