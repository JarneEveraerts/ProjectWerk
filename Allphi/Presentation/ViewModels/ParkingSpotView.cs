using Domain.Models;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class ParkingSpotView : ViewModelBase
    {
        private Employee? _employee;
        public Employee? Employee
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
        private Visitor? _visitor;
        public Visitor? Visitor
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

        private Business? _reserved;
        public Business? Reserved
        {
            get { return _reserved; }
            set
            {
                if (_reserved != value)
                {
                    _reserved = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string? _plate;
        public string? Plate
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

        public ParkingSpotView(Employee? employee, Visitor? visitor, string? plate, Business? reserved)
        {
            Employee = employee;
            Visitor = visitor;
            Plate = plate;
            Reserved = reserved;
        }
    }
}
