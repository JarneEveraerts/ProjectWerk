using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class Contract
    {
        public int Id
        {
            get; set;
        }
        public Business Business
        {
            get; set;
        }
        public DateTime StartDate
        {
            get; set;
        }
        public DateTime EndDate
        {
            get; set;
        }
        public int TotalSpaces
        {
            get; set;
        }
        public bool IsDeleted
        {
            get; set;
        }
    }
}
