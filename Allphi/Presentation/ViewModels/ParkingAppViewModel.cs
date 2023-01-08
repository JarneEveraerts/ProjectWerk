using Domain;
using JetBrains.Annotations;
using MVVM;
using Newtonsoft.Json;
using Shared.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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
        private RelayCommand _submitCommand;
        private bool _canSubmit;
        private ObservableCollection<string> _businessesViews;
        private HttpClient _api;
        private ViewController _vc;
        #endregion


        #region public properties
        [Required(ErrorMessage = "nummerplaat is verplicht")]
        [MethodValidator(nameof(IsPlateValid))]
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
        [Required(ErrorMessage = "bedrijf is verplicht")]
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

        public ObservableCollection<string> BusinessViews
        {
            get
            {
                return _businessesViews;
            }
            set
            {
                _businessesViews = value;
                RaisePropertyChanged();
            }
        }


        public ICommand SubmitCommand
        {
            get
            {
                //check if the fields has errors else cansubmit is true
                _submitCommand ??= new RelayCommand(Submit, param => CanSubmit());
                return _submitCommand;
            }
        }

        private string IsPlateValid()
        {
            if (!(Plate == null))
            {

                if (!_vc.IsLicensePlateValid(Plate))
                {
                    return "Nummerplaat is niet geldig";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        private bool CanSubmit()
        {
            if (string.IsNullOrEmpty(Plate) && (string.IsNullOrEmpty(Business) && !_vc.IsLicensePlateValid(Plate)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        public ParkingAppViewModel(ViewController vc, IHttpClientFactory clientFactory)
        {
            _vc = vc;
            _businessesViews = new();
            _api = clientFactory.CreateClient();
            _api.BaseAddress = new Uri("http://localhost:5038/");
            FetchBusinesses();
        }

        private void FetchBusinesses()
        {
            //fetch businesses
            var response = _api.GetAsync("/businesses").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var businesses = JsonConvert.DeserializeObject<List<BusinessDto>>(content);

            foreach (var item in businesses)
            {
                BusinessViews.Add(item.Name);
            }

        }

        private void Submit(object parameter)
        {
            if (!HasErrors)
            {
                
                _vc.EnterParking(Plate, Business);

            }
        }

    }


}

