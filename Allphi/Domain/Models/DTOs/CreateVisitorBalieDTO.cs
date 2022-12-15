using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class CreateVisitorBalieDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Plate { get; set; }
        public string Business { get; set; }

    }
}
