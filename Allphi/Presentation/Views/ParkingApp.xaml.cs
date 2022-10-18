using System.Windows;
using Ardalis.GuardClauses;
using Domain;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for ParkingEntrance.xaml
    /// </summary>
    public partial class ParkingApp : Window
    {
        private DomainController _dc;

        private string _licensePlate;

        public ParkingApp(DomainController dc)
        {
            _dc = dc;
            InitializeComponent();
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
            lbl_BedrijfNL.Content = "Connexion/enregistrement en tant que";
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            Btn_Visitor.Content = "Bezoeker";
            lbl_LicensePlateNL.Content = "Nummerplaat";
            lbl_BedrijfNL.Content = "Login/ Register als";
        }

        private void Btn_Submit_Employee_Click(object sender, RoutedEventArgs e)
        {
            _licensePlate = txtBox_LicensePlate.Text;
            _dc.SubmitEmployeeParking(_licensePlate);
        }

        private void Btn_Submit_Visitor_Click(object sender, RoutedEventArgs e)
        {
            _licensePlate = txtBox_LicensePlate.Text;
            _dc.SubmitVisitorParking(_licensePlate);
        }
    }
}