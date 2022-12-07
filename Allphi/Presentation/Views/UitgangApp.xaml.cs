using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain;
using Newtonsoft.Json;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for UitgangApp.xaml
    /// </summary>
    public partial class UitgangApp : Window
    {
        private DomainController _dc;
        private HttpClient _apiClient;

        public UitgangApp(DomainController dc, IHttpClientFactory clientFactory)
        {
            _dc = dc;
            _apiClient = clientFactory.CreateClient();
            _apiClient.BaseAddress = new Uri("http://localhost:5269");
            InitializeComponent();
        }

        private async void Btn_ExitParking_Click(object sender, RoutedEventArgs e)
        {
            string input = txt_LicensePlate.Text;
            if (input == "" || !_dc.IsLicensePlateValid(input))
            {
                MessageBox.Show("License plate is not valid");
                return;
            }
            var parkingSpotResponse = await _apiClient.PostAsync($"/parkingspots/exit/{input}", null);
            var parkingSpotContentString = parkingSpotResponse.Content.ReadAsStringAsync().Result;
            var exitedSpot = JsonConvert.DeserializeObject<bool>(parkingSpotContentString);
            if (exitedSpot)
            {
                MessageBox.Show("Have A Nice Trip");
            }
            else
            {
                MessageBox.Show("License plate is not valid");
            }
        }
    }
}