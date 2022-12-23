using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using Ardalis.GuardClauses;
using Domain;
using Newtonsoft.Json;
using Persistance;
using Presentation.ViewModels;
using Shared.Dto;
using static System.Net.WebRequestMethods;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for ParkingEntrance.xaml
    /// </summary>
    public partial class ParkingApp : Window
    {
        private DomainController _dc;
        private List<List<string>> businesses;
        private List<BusinessView>? businessViews = new();
        private string _licensePlate;
        private HttpClient _api;

        public ParkingApp(DomainController dc, IHttpClientFactory clientFactory)
        {
            DataContext = new ParkingAppViewModel();
            _api = clientFactory.CreateClient();
            _api.BaseAddress = new Uri("http://localhost:5038");
            InitializeComponent();
            _dc = dc;

            List<BusinessView> businessViews = new();
            string uri = "/Businesses";
            var result = _api.GetAsync(uri).Result;
            var content = result.Content.ReadAsStringAsync().Result;
            var businesses = JsonConvert.DeserializeObject<List<BusinessDto>>(content);

            foreach (var item in businesses)
            {
                businessViews.Add(new BusinessView(item));
                cmb_business.Items.Add(item.Name);
            }
        }

        private string Bussines { get; set; }

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
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            Btn_Visitor.Content = "Bezoeker";
            lbl_LicensePlateNL.Content = "Nummerplaat";
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
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
            if (_dc.EnterParking(_licensePlate, business))
            {
                MessageBox.Show("Welcome");
            }
            else
            {
                MessageBox.Show("No Free Spots");
            }
        }
    }
}