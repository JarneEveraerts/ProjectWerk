using Domain.Models;

namespace Presentation.ViewModels
{
    public class EmployeeView : ViewModelBase
    {
        private int _id;
        private string _name;
        private string _firstName;
        private string _function;
        private Business _business;
        private string _email;
        private string _plate;
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

        public string? Email
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

        public EmployeeView(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            FirstName = employee.FirstName;
            Function = employee.Function;
            Business = employee.Business;
            Email = employee.Email;
            Plate = employee.Plate;
            IsDeleted = employee.IsDeleted;
        }
        public EmployeeView()
        {

        }
    }
}