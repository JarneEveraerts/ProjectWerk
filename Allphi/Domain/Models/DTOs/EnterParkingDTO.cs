using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class EnterParkingDTO
    {
        public string Plate { get; set; }
        public Business Business { get; set; }
        public Employee? Employee { get; set; }
        public Visitor? Visitor { get; set; }
        public Contract Contract { get; set; }
    }
}
