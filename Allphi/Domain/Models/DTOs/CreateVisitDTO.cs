using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class CreateVisitDTO
    {
        public string Visitor { get; set; }
        public string Employee { get; set; }
        public string Business { get; set; }
    }
}
