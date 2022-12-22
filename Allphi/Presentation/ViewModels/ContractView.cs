using Domain.Models;
using MVVM;
using System;

namespace Presentation.ViewModels
{
    public class ContractView : ViewModelBase
    {
        private int _id;
        private Business _business;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _totalSpaces;
        private bool _isDeleted;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        public ContractView(Contract contract)
        {
            Id = contract.Id;
            Business = contract.Business;
            StartDate = contract.StartDate;
            EndDate = contract.EndDate;
            TotalSpaces = contract.TotalSpaces;
            IsDeleted = contract.IsDeleted;
        }
    }
}