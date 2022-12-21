using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class ParkingSpot
    {
        public int Id
        {
            get; set;
        }
        public Employee? Employee
        {
            get; set;
        }
        public Visitor? Visitor
        {
            get; set;
        }
        public Business? Reserved
        {
            get; set;
        }
        public string? Plate
        {
            get; set;
        }
    }
}
