using System.Windows;
using Domain;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for LoginAdmin.xaml
    /// </summary>
    public partial class LoginAdmin : Window
    {
        private readonly DomainController _dc;

        public LoginAdmin()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}