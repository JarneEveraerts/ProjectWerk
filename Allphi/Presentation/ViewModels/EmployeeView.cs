using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class EmployeeView : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _function;
        public string Function
        {
            get { return _function; }
            set
            {
                if (_function != value)
                {
                    _function = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _plate;
        public string Plate
        {
            get { return _plate; }
            set
            {
                if (_plate != value)
                {
                    _plate = value;
                    RaisePropertyChanged();
                }
            }
        }


        private Business _business;
        public Business Business
        {
            get { return _business; }
            set
            {
                if (_business != value)
                {
                    _business = value;
                    RaisePropertyChanged();
                }
            }
        }


        public EmployeeView(string name, string firstname, string function, Business business, string? email, string? plate)
        {
            Name = name;
            FirstName = firstname;
            Function = function;
            Business = business;
            Email = email;
            Plate = plate;
        }

    }
}
