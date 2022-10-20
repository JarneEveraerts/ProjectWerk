using Domain.Models;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class BusinessView : ViewModelBase
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
        private string _btw;
        public string Btw
        {
            get { return _btw; }
            set
            {
                if (_btw != value)
                {
                    _btw = value;
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

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    RaisePropertyChanged();
                }
            }
        }


        private bool _isDeleted;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                if (_isDeleted != value)
                {
                    _isDeleted = value;
                    RaisePropertyChanged();
                }
            }
        }


        private Business _business ;
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


        public BusinessView(string name, string btw, string email, string? address, string? phone, bool isDeleted)
        {
            Name = name;
            Btw = btw;
            Email = email;
            Address = address;
            Phone = phone;
            IsDeleted = isDeleted;
        }
        public BusinessView(Business business)
        {
            Business = business;

        }
    }
}
