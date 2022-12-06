using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using Ardalis.GuardClauses;
using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Newtonsoft.Json;
using Persistance;
using Presentation.ViewModels;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for ParkingEntrance.xaml
    /// </summary>
    public partial class ParkingApp : Window
    {
        private DomainController _dc;
        private List<Business> _businesses = new List<Business>();
        private List<BusinessView>? businessViews = new();
        private List<Contract> _contracts= new List<Contract>();
        private string _licensePlate;
        private HttpClient _apiClient;


        public ParkingApp(DomainController dc, IHttpClientFactory clientFactory)
        {
            InitializeComponent();
            _dc = dc;
            _apiClient = clientFactory.CreateClient();
            _apiClient.BaseAddress = new Uri("http://localhost:5269");
            var businessesResponse = _apiClient.GetAsync("/businesses").Result;
            var contentString = businessesResponse.Content.ReadAsStringAsync().Result;
            var businesses = JsonConvert.DeserializeObject<List<Business>>(contentString);
            _businesses = businesses;
            var contractResponse = _apiClient.GetAsync("/contracts").Result;
            var contractContentString = contractResponse.Content.ReadAsStringAsync().Result;
            var contracts = JsonConvert.DeserializeObject<List<Contract>>(contractContentString);
            _contracts = contracts;

            if (businesses.Count != 0)
            {
                foreach (var item in businesses)
                {
                    businessViews.Add(new BusinessView(item));
                    cmb_business.Items.Add(item.Name);
                }
            }
        }

        private void Btn_ENG_Click(object sender, RoutedEventArgs e)
        {
            Btn_Visitor.Content = "Visitor";
            lbl_LicensePlateNL.Content = "License Plate";
            lbl_LicensePlateNL.Content = "Login/Register as";
        }

        private void Btn_FR_Click(object sender, RoutedEventArgs e)
        {
            Btn_Visitor.Content = "Visiteur";
            lbl_LicensePlateNL.Content = "Plaque d'immatriculation";
            lbl_BedrijfNL.Content = "Connexion/enregistrement en tant que";
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            Btn_Visitor.Content = "Bezoeker";
            lbl_LicensePlateNL.Content = "Nummerplaat";
            lbl_BedrijfNL.Content = "Login/ Register als";
        }

        private async void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            _licensePlate = txtBox_LicensePlate.Text;
            string business = cmb_business.Text;

            if (_licensePlate == "" || !_dc.IsLicensePlateValid(_licensePlate))
            {
                MessageBox.Show("License plate is not valid");
                return;
            }
            if (cmb_business.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a business");
                return;
            }

            var businessObject = _businesses.SingleOrDefault(b => b.Name == business);

            var contract = _contracts.SingleOrDefault(c => c.Business.Id == businessObject.Id);


            var employeeResponse = await _apiClient.GetAsync($"/employees/licenseplate/{_licensePlate}");
            var contentStringEmployee = await employeeResponse.Content.ReadAsStringAsync();
            var employee = JsonConvert.DeserializeObject<Employee>(contentStringEmployee);


            var enterParking = new EnterParkingDTO
            {
                Plate = _licensePlate,
                Employee = employee,
                Contract = contract,
                Business = businessObject
            };

            var enterParkingString = JsonConvert.SerializeObject(enterParking);
            var parkingSpotResponse = await _apiClient.PostAsync($"/parkingspots/enter", new StringContent(enterParkingString, Encoding.UTF8, "application/json"));
            if (parkingSpotResponse.IsSuccessStatusCode)
            {
                var parkingSpotContentString = parkingSpotResponse.Content.ReadAsStringAsync().Result;
                var spotExists = JsonConvert.DeserializeObject<bool>(parkingSpotContentString);
                if (spotExists)
                {
                    MessageBox.Show("Welcome");
                }
                else
                {
                    MessageBox.Show("No Free Spots");
                }

            }
            else
            {
                MessageBox.Show($"Error: {parkingSpotResponse.StatusCode}");
            }

        }
    }
}