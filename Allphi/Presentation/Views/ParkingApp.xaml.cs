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
            Btn_NL.Visibility = Visibility.Visible;
            Btn_FR.Visibility = Visibility.Visible;
            Btn_ENG.Visibility = Visibility.Collapsed;
            lbl_LicensePlateNL.Visibility = Visibility.Collapsed;
            lbl_LicensePlateFR.Visibility = Visibility.Collapsed;
            lbl_LicensePlateENG.Visibility = Visibility.Visible;
            Btn_SubmitEmployeeNL.Visibility = Visibility.Collapsed;
            Btn_SubmitEmployeeFR.Visibility = Visibility.Collapsed;
            Btn_SubmitEmployeeENG.Visibility = Visibility.Visible;
        }

        private void Btn_FR_Click(object sender, RoutedEventArgs e)
        {
            Btn_NL.Visibility = Visibility.Visible;
            Btn_FR.Visibility = Visibility.Collapsed;
            Btn_ENG.Visibility = Visibility.Visible;
            lbl_LicensePlateNL.Visibility = Visibility.Collapsed;
            lbl_LicensePlateFR.Visibility = Visibility.Visible;
            lbl_LicensePlateENG.Visibility = Visibility.Collapsed;
            Btn_SubmitEmployeeNL.Visibility = Visibility.Collapsed;
            Btn_SubmitEmployeeFR.Visibility = Visibility.Visible;
            Btn_SubmitEmployeeENG.Visibility = Visibility.Collapsed;
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            Btn_NL.Visibility = Visibility.Collapsed;
            Btn_FR.Visibility = Visibility.Visible;
            Btn_ENG.Visibility = Visibility.Visible;
            lbl_LicensePlateNL.Visibility = Visibility.Visible;
            lbl_LicensePlateFR.Visibility = Visibility.Collapsed;
            lbl_LicensePlateENG.Visibility = Visibility.Collapsed;
            Btn_SubmitEmployeeNL.Visibility = Visibility.Visible;
            Btn_SubmitEmployeeFR.Visibility = Visibility.Collapsed;
            Btn_SubmitEmployeeENG.Visibility = Visibility.Collapsed;
        }

        private void Btn_Submit_Employee_Click(object sender, RoutedEventArgs e)
        {
            _licensePlate = txtBox_LicensePlate.Text;
            _dc.SubmitVisitor(_licensePlate);
        }

        private void Btn_Submit_Visitor_Click(object sender, RoutedEventArgs e)
        {
            _licensePlate = txtBox_LicensePlate.Text;
            _dc.SubmitVisitor(_licensePlate);
        }
    }
}