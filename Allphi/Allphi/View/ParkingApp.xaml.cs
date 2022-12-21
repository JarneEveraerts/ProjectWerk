using System;
using System.Collections.Generic;
using System.Linq;
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
using Domain.Models;
using Domain.Services;
using Domain.Data.Repositorys;

namespace Domain.View
{
    /// <summary>
    /// Interaction logic for ParkingEntrance.xaml
    /// </summary>
    public partial class ParkingApp : Window
    {
        public IParkingRepository parkingRepository;

        public ParkingApp()
        {
            parkingRepository = new ParkingRepository();
            InitializeComponent();
        }

        private string Bussines { get; set; }

        private void Btn_ENG_Click(object sender, RoutedEventArgs e)
        {
            Btn_NL.Visibility = Visibility.Visible;
            Btn_FR.Visibility = Visibility.Visible;
            Btn_ENG.Visibility = Visibility.Collapsed;
            lbl_LicensePlateNL.Visibility = Visibility.Collapsed;
            lbl_LicensePlateFR.Visibility = Visibility.Collapsed;
            lbl_LicensePlateENG.Visibility = Visibility.Visible;
            Btn_SubmitNL.Visibility = Visibility.Collapsed;
            Btn_SubmitFR.Visibility = Visibility.Collapsed;
            Btn_SubmitENG.Visibility = Visibility.Visible;
        }

        private void Btn_FR_Click(object sender, RoutedEventArgs e)
        {
            Btn_NL.Visibility = Visibility.Visible;
            Btn_FR.Visibility = Visibility.Collapsed;
            Btn_ENG.Visibility = Visibility.Visible;
            lbl_LicensePlateNL.Visibility = Visibility.Collapsed;
            lbl_LicensePlateFR.Visibility = Visibility.Visible;
            lbl_LicensePlateENG.Visibility = Visibility.Collapsed;
            Btn_SubmitNL.Visibility = Visibility.Collapsed;
            Btn_SubmitFR.Visibility = Visibility.Visible;
            Btn_SubmitENG.Visibility = Visibility.Collapsed;
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            Btn_NL.Visibility = Visibility.Collapsed;
            Btn_FR.Visibility = Visibility.Visible;
            Btn_ENG.Visibility = Visibility.Visible;
            lbl_LicensePlateNL.Visibility = Visibility.Visible;
            lbl_LicensePlateFR.Visibility = Visibility.Collapsed;
            lbl_LicensePlateENG.Visibility = Visibility.Collapsed;
            Btn_SubmitNL.Visibility = Visibility.Visible;
            Btn_SubmitFR.Visibility = Visibility.Collapsed;
            Btn_SubmitENG.Visibility = Visibility.Collapsed;
        }

        private void RBtn_Allphi_Checked(object sender, RoutedEventArgs e)
        {
            Bussines = Rbtn_AllPhi.Content.ToString();
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Parking parking;
            parking = new Parking(txtBox_LicensePlate.Text);

            parkingRepository.AddParking(parking);
        }
    }
}