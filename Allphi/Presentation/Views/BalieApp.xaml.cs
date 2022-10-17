using Domain;
using System.Windows;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for BalieApp.xaml
    /// </summary>
    public partial class BalieApp : Window
    {
        private DomainController _dc;

        public BalieApp(DomainController dc)
        {
            _dc = dc;
            InitializeComponent();
        }
    }
}