using Domain.Models;
using MVVM;
using Shared.Dto;

namespace Presentation.ViewModels
{
    public class ParkingSpotView : ViewModelBase
    {
        private int _id;
        private EmployeeDto? _employee;
        private VisitorDto? _visitor;
        private int _reservedId;
        private BusinessDto? _reserved;
        private string? _plate;
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

        public EmployeeDto? Employee
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

        public VisitorDto? Visitor
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

        public int ReservedId
        {
            get { return _reservedId; }
            set
            {
                if (_reservedId != value)
                {
                    _reservedId = value;
                    RaisePropertyChanged();
                }
            }
        }

        public BusinessDto? Reserved
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

        public ParkingSpotView(ParkingSpotDto parkingSpot)
        {
            Id = parkingSpot.Id;
            Employee = parkingSpot.Employee;
            Visitor = parkingSpot.Visitor;
            Plate = parkingSpot.Plate;
            ReservedId = parkingSpot.ReservedId;
            Reserved = parkingSpot.Reserved;
            IsDeleted = parkingSpot.IsDeleted;
        }
    }
}