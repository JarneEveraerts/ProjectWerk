using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllphiDB.Models
{
    internal class Employee
    {
        public Employee(string name, string firstname, string function, Business business)
        {
            Name = name;
            FirstName = firstname;
            Function = function;
            Business = business;
        }

        public Employee(string name, string firstname, string function, string email, Business business)
        {
            Name = name;
            FirstName = firstname;
            Email = email;
            Function = function;
            Business = business;
        }

        public Employee()
        {
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Function { get; set; }
        public Business Business { get; set; }
    }
}