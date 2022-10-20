using Domain.Models;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class ContractView : ViewModelBase
    {
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
        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
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

        private int _totalSpaces;
        public int TotalSpaces
        {
            get { return _totalSpaces; }
            set
            {
                if (_totalSpaces != value)
                {
                    _totalSpaces = value;
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

        public ContractView(Business business, DateTime startDate, DateTime endDate, int totalSpaces)
        {
            Business = business;
            StartDate = startDate;
            EndDate = endDate;
            TotalSpaces = totalSpaces;
        }
    }
}
