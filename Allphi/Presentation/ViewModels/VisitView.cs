using Domain.Models;
using MVVM;
using Shared.Dto;
using System;

namespace Presentation.ViewModels
{
    public class VisitView : ViewModelBase
    {
        private int _id;
        private VisitorDto _visitor;
        private DateTime _startDate;
        private DateTime? _endDate;
        private EmployeeDto _employee;
        private BusinessDto _business;
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

        public VisitorDto Visitor
        {
            get { return _visitor; }
            set
            {
                if (_visitor != value)
                {
                    _visitor = value;
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

        public DateTime? EndDate
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

        public EmployeeDto Employee
        {
            get { return _employee; }
            set
            {
                if (_employee != value)
                {
                    _employee = value;
                    RaisePropertyChanged();
                }
            }
        }

        public BusinessDto Business
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

        public VisitView(VisitDto visit)
        {
            Id = visit.Id;
            Visitor = visit.Visitor;
            StartDate = visit.StartDate;
            EndDate = visit.EndDate;
            Employee = visit.Employee;
            Business = visit.Business;
            IsDeleted = visit.IsDeleted;
        }
    }
}