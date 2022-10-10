using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllphiDB.Models
{
    internal class Visitor
    {
        public Visitor(string name, string email, string business)
        {
            Name = name;
            Email = email;
            Business = business;
        }

        public int VisitorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Business { get; set; }
    }
}