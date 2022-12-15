using Domain.Models;

namespace Presentation.ViewModels
{
    public class VisitorView : ViewModelBase
    {
        private int _id;
        private string _name;
        private string _email;
        private string? _plate;
        private string _business;
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

        public string Business
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

        public VisitorView(int id, string name, string email, string? plate, string business, bool isDeleted)
        {
            Id = id;
            Name = name;
            Email = email;
            Plate = plate;
            Business = business;
            IsDeleted = isDeleted;
  
        }

        public VisitorView(Visitor visitor)
        {
            Id = visitor.Id;
            Name = visitor.Name;
            Email = visitor.Email;
            Business = visitor.Business;
            Plate = visitor.Plate;
            IsDeleted = visitor.IsDeleted;
        }
        public VisitorView()
        {
                
        }
    }
}