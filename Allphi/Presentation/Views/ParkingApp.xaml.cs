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
        private HttpClient _api = new HttpClient();

        public ParkingApp(DomainController dc, IHttpClientFactory clientFactory)
        {
            DataContext = new ParkingAppViewModel(clientFactory);
            _api = clientFactory.CreateClient();
            _api.BaseAddress = new Uri("http://localhost:5038");
            InitializeComponent();
            _dc = dc;
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
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            Btn_Visitor.Content = "Bezoeker";
            lbl_LicensePlateNL.Content = "Nummerplaat";
        }

 
    }
}