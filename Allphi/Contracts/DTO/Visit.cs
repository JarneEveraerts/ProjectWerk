using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class Visit
    {
        public int Id { get; set; }
        public Visitor Visitor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Employee Employee { get; set; }
        public Business Business { get; set; }
        public bool IsDeleted { get; set; }

    }
}
