﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class Employee
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string FirstName
        {
            get; set;
        }
        public string Function
        {
            get; set;
        }
        public Business Business
        {
            get; set;
        }
        public string Email { get; set; }
        public string Plate { get; set; }
        public bool IsDeleted
        {
            get; set;
        }
    }
}