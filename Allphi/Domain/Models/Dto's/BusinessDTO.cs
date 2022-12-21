using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto_s
{
    public class BusinessDTO : EntityDTO
    {
        public string Name { get; set; }
        public string Btw { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsDeleted { get; set; }
    }
}