using JetBrains.Annotations;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class ParkingAppViewModel : ViewModelBase
    {
        #region private properties

        private string _plate;
        private string _business;
        #endregion


        #region public properties
        public string Plate
        {
            get
            {
                return _plate;
            }
            set
            {
                _plate = value;
                RaisePropertyChanged(); 
            }
        }
        public string Business
        {
            get
            {
                return _business;
            }
            set
            {
                _business = value;
                RaisePropertyChanged();
            }
        }
        

        #endregion
        public ParkingAppViewModel()
        {
        }

        private RelayCommand _fetchCommand;
        public ICommand FetchCommand
        {
            get
            {
                _fetchCommand ??= new RelayCommand(Fetch, param => CanFetch);
                return _fetchCommand;
            }
        }
        private bool _canFetch = true;

        public bool CanFetch
        {
            get => _canFetch;

            set
            {
                if (_canFetch == value) return;
                _canFetch = value;

                RaisePropertyChanged();
            }
        }
        private void Validate(object paramater)
        {
            //validate plate and business
            
            
            

        }

        private void Fetch(object parameter)
        {
            //fetch data from api/domaincontroller
            
        }

    }
}
