using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class UpdateContractDTO
    {
        public Contract Contract { get; set; }
        public Business Business { get; set; }
        public int Spots { get; set; }
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
