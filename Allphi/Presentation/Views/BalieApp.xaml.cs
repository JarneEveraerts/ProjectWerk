using System;
using System.Collections.Generic;
using Domain;
using System.Windows;
using Domain.Models;
using Persistance;
using Presentation.ViewModels;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for BalieApp.xaml
    /// </summary>
    public partial class BalieApp : Window
    {
        private DomainController _dc;
        private List<BusinessView>? _businessViews = new();
        private List<ContractView>? _contractViews = new();
        private List<EmployeeView>? _employeeViews = new();
        private List<ParkingSpotView>? _parkingSpotViews = new();
        private List<VisitorView>?  _visitorViews = new();
        private List<VisitView>? _visitViews = new();

        public BalieApp(DomainController dc)
        {
            _dc = dc;
            InitializeComponent();
            SetupDataGrid();
        }

        private void SetupDataGrid()
        {
            dtg_businesses.ItemsSource = _dc.GetBusinesses();
            dtg_visitors.ItemsSource = _dc.GetVisitors();
            dtg_contracts.ItemsSource = _dc.GetContracts();
            dtg_employees.ItemsSource = _dc.GetEmployees();
        }

        #region UPDATE

        private void btn_updateBusiness_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_business_name.Text;
            string? _address = txt_business_address.Text;
            string? _phone = txt_business_phone.Text;
            string _email = txt_business_email.Text;
            string _btw = txt_business_btw.Text;
            _dc.UpdateBusiness(_name, _address, _phone, _email, _btw, (Business)dtg_businesses.SelectedItem);
            SetupDataGrid();
        }

        private void btn_updateVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;
            _dc.UpdateVisitor(_name, _email, _plate, _business, (Visitor)dtg_visitors.SelectedItem);
            SetupDataGrid();
        }

        private void btn_updateContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            _dc.UpdateContract(_spots, _business, _start, _end, (Contract)dtg_contracts.SelectedItem);
            SetupDataGrid();
        }

        private void btn_updateEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;
            _dc.UpdateEmployee(_name, _email, _function, _business, _plate, (Employee)dtg_employees.SelectedItem);
            SetupDataGrid();
        }

        #endregion UPDATE

        #region ADD

        private void btn_addBusiness_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_business_name.Text;
            string? _address = txt_business_address.Text;
            string? _phone = txt_business_phone.Text;
            string _email = txt_business_email.Text;
            string _btw = txt_business_btw.Text;
            _dc.CreateBusiness(_name, _address, _phone, _email, _btw);
            SetupDataGrid();
        }

        private void btn_addVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;
            _dc.CreateVisitor(_name, _email, _plate, _business);
            SetupDataGrid();
        }

        private void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            _dc.CreateContract(_spots, _business, _start, _end);
            SetupDataGrid();
        }

        private void btn_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;
            _dc.CreateEmployee(_name, _email, _function, _business, _plate);
            SetupDataGrid();
        }

        #endregion ADD

        #region SelectionChanged

        private void dtg_businesses_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dtg_businesses.SelectedItem != null)
            {
                List<string> selected = _dc.ConvertBusinessToStringList((Business)dtg_businesses.SelectedItem);
                txt_business_name.Text = selected[0];
                txt_business_address.Text = selected[1];
                txt_business_phone.Text = selected[2];
                txt_business_email.Text = selected[3];
                txt_business_btw.Text = selected[4];
            }
        }

        private void dtg_visitors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dtg_visitors.SelectedItem != null)
            {
                List<string> selected = _dc.ConvertVisitorToStringList((Visitor)dtg_visitors.SelectedItem);
                txt_visitor_name.Text = selected[0];
                txt_visitor_email.Text = selected[1];
                txt_visitor_plate.Text = selected[2];
                txt_visitor_business.Text = selected[3];
            }
        }

        private void dtg_contracts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dtg_contracts.SelectedItem != null)
            {
                List<string> selected = _dc.ConvertContractToStringList((Contract)dtg_contracts.SelectedItem);
                txt_contract_business.Text = selected[0];
                txt_contract_spots.Text = selected[1];
                dtp_start.SelectedDate = Convert.ToDateTime(selected[2]);
                dtp_end.SelectedDate = Convert.ToDateTime(selected[3]);
            }
        }

        private void dtg_employees_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dtg_employees.SelectedItem != null)
            {
                List<string> selected = _dc.ConvertEmployeeToStringList((Employee)dtg_employees.SelectedItem);
                txt_employee_name.Text = selected[0];
                txt_employee_email.Text = selected[1];
                txt_employee_business.Text = selected[2];
                txt_employee_function.Text = selected[3];
                txt_employee_plate.Text = selected[4];
            }
        }

        #endregion SelectionChanged

        #region DELETE

        private void btn_deleteVisitor_Click(object sender, RoutedEventArgs e)
        {
            _dc.DeleteVisitor((Visitor)dtg_visitors.SelectedItem);
            SetupDataGrid();
        }

        private void btn_deleteContract_Click(object sender, RoutedEventArgs e)
        {
            _dc.DeleteContract((Contract)dtg_contracts.SelectedItem);
            SetupDataGrid();
        }

        private void btn_deleteBusiness_Click(object sender, RoutedEventArgs e)
        {
            _dc.DeleteBusiness((Business)dtg_businesses.SelectedItem);
            SetupDataGrid();
        }

        private void btn_deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            _dc.DeleteEmployee((Employee)dtg_employees.SelectedItem);
            SetupDataGrid();
        }

        #endregion DELETE
    }
}