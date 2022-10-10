using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllphiDB.Models
{
    internal class Contract
    {
        public Contract(Business business, DateTime startDate, DateTime endDate, int totalSpaces)
        {
            Business = business;
            StartDate = startDate;
            EndDate = endDate;
            TotalSpaces = totalSpaces;
        }

        public Contract()
        {
        }

        public int ContractID { get; set; }
        public Business Business { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalSpaces { get; set; }
    }
}