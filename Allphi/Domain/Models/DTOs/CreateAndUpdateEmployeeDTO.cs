using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class CreateAndUpdateEmployeeDTO
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Function { get; set; }
        public string? Plate { get; set; }
        public Business Business { get; set; }
    }
}
