using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allphi.Models;

namespace Allphi.Models
{
    public class Parking : Entity
    {
        public Parking(Employee employee, Visitor visitor, string plate)
        {
            Employee = employee;
            Visitor = visitor;
            Plate = plate;
        }

        public Parking()
        {
        }

        public Visitor? Visitor { get; set; }
        public Employee? Employee { get; set; }
        public string Plate { get; set; }
    }
}