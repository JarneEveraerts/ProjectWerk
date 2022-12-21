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
using Domain;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for UitgangApp.xaml
    /// </summary>
    public partial class UitgangApp : Window
    {
        private DomainController _dc;

        public UitgangApp(DomainController dc)
        {
            _dc = dc;
            InitializeComponent();
        }

        private void Btn_ExitParking_Click(object sender, RoutedEventArgs e)
        {
            string input = txt_LicensePlate.Text;
            if (input == "" || !_dc.IsLicensePlateValid(input))
            {
                MessageBox.Show("License plate is not valid");
                return;
            }
            if (_dc.ExitParking(input))
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