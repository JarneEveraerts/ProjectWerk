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
        #endregion


        #region public properties
        [Required(ErrorMessage ="Nummerplaat mag niet leeg zijn")]
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
        [Required(ErrorMessage = "Bedrijf mag niet leeg zijn")]
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


        private bool CanSubmit()
        {
            if (string.IsNullOrEmpty(Plate) || string.IsNullOrEmpty(Business))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        public ParkingAppViewModel(IHttpClientFactory clientFactory)
        {
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

            }
        }

    }


}

