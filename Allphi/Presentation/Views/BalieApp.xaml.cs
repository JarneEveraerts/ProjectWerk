using System;
using System.Collections.Generic;
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
            dtg_businesses.ItemsSource = _dc.GetBusinesses();
            dtg_visitors.ItemsSource = _dc.GetVisitors();
            dtg_contracts.ItemsSource = _dc.GetContracts();
            dtg_employees.ItemsSource = _dc.GetEmployees();
        }

        private void btn_addBusiness_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_business_name.Text;
            string? _address = txt_business_address.Text;
            string? _phone = txt_business_phone.Text;
            string _email = txt_business_email.Text;
            string _btw = txt_business_btw.Text;
            _dc.CreateBusiness(_name, _address, _phone, _email, _btw);
        }

        private void btn_addVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;
            _dc.CreateVisitor(_name, _email, _plate, _business);
        }

        private void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            _dc.CreateContract(_spots, _business, _start, _end);
        }

        private void btn_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;
            _dc.CreateEmployee(_name, _email, _function, _business, _plate);
        }

        private void dtg_businesses_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List<string> selected = _dc.ConvertBusinessToStringList(dtg_businesses.SelectedItem);
            txt_business_name.Text = selected[0];
            txt_business_address.Text = selected[1];
            txt_business_phone.Text = selected[2];
            txt_business_email.Text = selected[3];
            txt_business_btw.Text = selected[4];
        }

        private void dtg_visitors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List<string> selected = _dc.ConvertVisitorToStringList(dtg_visitors.SelectedItem);
            txt_visitor_name.Text = selected[0];
            txt_visitor_email.Text = selected[1];
            txt_visitor_plate.Text = selected[2];
            txt_visitor_business.Text = selected[3];
        }

        private void dtg_contracts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List<string> selected = _dc.ConvertContractToStringList(dtg_contracts.SelectedItem);
            txt_contract_business.Text = selected[0];
            txt_contract_spots.Text = selected[1];
            dtp_start.SelectedDate = Convert.ToDateTime(selected[2]);
            dtp_end.SelectedDate = Convert.ToDateTime(selected[3]);
        }

        private void dtg_employees_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List<string> selected = _dc.ConvertEmployeeToStringList(dtg_employees.SelectedItem);
            txt_employee_name.Text = selected[0];
            txt_employee_email.Text = selected[1];
            txt_employee_business.Text = selected[2];
            txt_employee_function.Text = selected[3];
            txt_employee_plate.Text = selected[4];
        }
    }
}