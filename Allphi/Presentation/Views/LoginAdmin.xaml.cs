using System.Windows;
using Domain;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for LoginAdmin.xaml
    /// </summary>
    public partial class LoginAdmin : Window
    {
        private readonly ViewController _vc;

        public LoginAdmin(ViewController vc)
        {
            _vc = vc;
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}