using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class UpdateVisitDTO
    {
        public Visitor Visitor { get; set; }
        public Employee Employee { get; set; }
        public Business Business { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
