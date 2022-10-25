using Domain;
using Domain.Models;
using Presentation.ViewModels;
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

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for VisitorRegistration.xaml
    /// </summary>
    public partial class VisitorRegistration : Window
    {
        private DomainController _dc;
        private List<List<string>> businesses;
        private List<BusinessView>? businessViews = new();
        private List<VisitView>? visitViews = new();
        private List<VisitorView>? visitorViews = new();
        private List<EmployeeView>? employeeViews = new();
        private VisitorView Visitor;
        public VisitorRegistration(DomainController dc)
        {
            InitializeComponent();
            _dc = dc;

            businesses = _dc.GiveBusinesses();
            if (businesses.Count != 0)
            {
                foreach (var item in _dc.GetBusinesses())
                {
                    businessViews.Add(new BusinessView(item));
                    cmb_business.Items.Add(item.Name);
                }
            }
            if (_dc.GetVisits().Count != 0)
            {
                foreach (var item in _dc.GetVisits())
                {
                    visitViews.Add(new VisitView(item));
                }
            }
            if (_dc.GetVisitors().Count != 0)
            {
                foreach (var item in _dc.GetVisitors())
                {
                    visitorViews.Add(new VisitorView(item));
                }
            }
            if (_dc.GetEmployees().Count != 0)
            {
                foreach (var item in _dc.GetEmployees())
                {
                    employeeViews.Add(new EmployeeView(item));
                    cmb_employees.Items.Add(item.Name);
                }
            }
        }

        private void btn_Registreren_Click(object sender, RoutedEventArgs e)
        {

            string _businessName = cmb_business.SelectedItem.ToString();
            string _employeeName = cmb_employees.SelectedItem.ToString();
            string _visitorName = txt_name.Text;
            string _visitorEmail = txt_email.Text;
            string _visitorPlate = txt_plate.Text;
            DateTime _startDate = DateTime.Now;
            List<VisitorView> doesVisitorExist = visitorViews.Where(x => x.Name == txt_name.Text).ToList();
            if (doesVisitorExist.Count == 0)
            {
               int id = _dc.CreateVisitor(_visitorName, _visitorEmail, _visitorPlate, _businessName);
                visitorViews.Add(new VisitorView(id, _visitorName, _visitorEmail, _visitorPlate, _businessName, false));


                _dc.CreateVisit(_visitorName, _businessName, _employeeName, _startDate, null);
            }
            else
            {

                _dc.CreateVisit(_visitorName, _businessName, _employeeName, _startDate, null);

            }
        }
    }
}
