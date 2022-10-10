using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllphiDB.Models
{
    internal class Business
    {
        public Business(string name, string btw, string email)
        {
            Name = name;
            Btw = btw;
            Email = email;
        }

        public Business(string name, string btw, string email, string adress, string phone)
        {
            Name = name;
            Btw = btw;
            Email = email;
            Adress = adress;
            Phone = phone;
        }

        public int BusinessID { get; set; }
        public string Name { get; set; }
        public string Btw { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}