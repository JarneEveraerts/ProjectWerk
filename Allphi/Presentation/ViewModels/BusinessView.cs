using Domain.Models;

namespace Presentation.ViewModels
{
    public class BusinessView : ViewModelBase
    {
        private int _id;
        private string _name;
        private string _btw;
        private string _email;
        private string? _address;
        private string? _phone;
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

        public BusinessView(Business business)
        {
            Id = business.Id;
            Name = business.Name;
            Btw = business.Btw;
            Email = business.Email;
            Address = business.Address;
            Phone = business.Phone;
            IsDeleted = business.IsDeleted;
        }
        public BusinessView()
        {

        }
    }
}