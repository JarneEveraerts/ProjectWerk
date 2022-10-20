using Domain.Models;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class VisitView:ViewModelBase
    {

        private Visitor _visitor;
        public Visitor Visitor
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

        private DateTime? _endDate;
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

        private Employee _employee;
        public Employee Employee
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


        public VisitView(Visitor visitor, Business business, Employee employee, DateTime startDate, DateTime? endDate)
        {
            Visitor = visitor;
            Business = business;
            Employee = employee;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
