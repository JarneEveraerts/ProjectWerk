using System;
using System.Collections.Generic;
using Domain;
using System.Windows;
using Domain.Models;
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
        private List<VisitorView>? _visitorViews = new();
        private List<VisitView>? _visitViews = new();

        public BalieApp(DomainController dc)
        {
            _dc = dc;
            InitializeComponent();
            SetupView();
        }

        private void SetupView()
        {
            _businessViews = GetBusinessViews();
            _contractViews = GetContractViews();
            _employeeViews = GetEmployeeViews();
            _parkingSpotViews = GetParkingSpotViews();
            _visitorViews = GetVisitorViews();
            _visitViews = GetVisitViews();
            dtg_businesses.ItemsSource = _businessViews;
            dtg_contracts.ItemsSource = _contractViews;
            dtg_employees.ItemsSource = _employeeViews;
            dtg_visitors.ItemsSource = _visitorViews;
        }

        #region GetViews

        private List<BusinessView> GetBusinessViews()
        {
            List<BusinessView> businessViews = new();
            foreach (var item in _dc.GetBusinesses())
            {
                businessViews.Add(new BusinessView(item));
            }
            return businessViews;
        }

        private List<ContractView> GetContractViews()
        {
            List<ContractView> contractViews = new();
            foreach (var item in _dc.GetContracts())
            {
                contractViews.Add(new ContractView(item));
            }
            return contractViews;
        }

        private List<EmployeeView> GetEmployeeViews()
        {
            List<EmployeeView> employeeViews = new();
            foreach (var item in _dc.GetEmployees())
            {
                employeeViews.Add(new EmployeeView(item));
            }
            return employeeViews;
        }

        private List<ParkingSpotView> GetParkingSpotViews()
        {
            List<ParkingSpotView> parkingSpotViews = new();
            foreach (var item in _dc.GetParkingSpots())
            {
                parkingSpotViews.Add(new ParkingSpotView(item));
            }
            return parkingSpotViews;
        }

        private List<VisitorView> GetVisitorViews()
        {
            List<VisitorView> visitorViews = new();
            foreach (var item in _dc.GetVisitors())
            {
                visitorViews.Add(new VisitorView(item));
            }
            return visitorViews;
        }

        private List<VisitView> GetVisitViews()
        {
            List<VisitView> visitViews = new();
            foreach (var item in _dc.GetVisits())
            {
                visitViews.Add(new VisitView(item));
            }
            return visitViews;
        }

        #endregion GetViews

        #region UPDATE

        private void btn_updateBusiness_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_business_name.Text;
            string? _address = txt_business_address.Text;
            string? _phone = txt_business_phone.Text;
            string _email = txt_business_email.Text;
            string _btw = txt_business_btw.Text;
            BusinessView businessView = (BusinessView)dtg_businesses.SelectedItem;
            _dc.UpdateBusiness(_name, _address, _phone, _email, _btw, (int)businessView.Id);
            SetupView();
        }

        private void btn_updateVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;
            VisitorView visitorView = (VisitorView)dtg_visitors.SelectedItem;
            _dc.UpdateVisitor(_name, _email, _plate, _business, (int)visitorView.Id);
            SetupView();
        }

        private void btn_updateContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            ContractView contractView = (ContractView)dtg_contracts.SelectedItem;
            _dc.UpdateContract(_spots, _business, _start, _end, (int)contractView.Id);
            SetupView();
        }

        private void btn_updateEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;
            EmployeeView employeeView = (EmployeeView)dtg_employees.SelectedItem;
            _dc.UpdateEmployee(_name, _email, _function, _business, _plate, (int)employeeView.Id);
            SetupView();
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
            SetupView();
        }

        private void btn_addVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;
            _dc.CreateVisitor(_name, _email, _plate, _business);
            SetupView();
        }

        private void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            _dc.CreateContract(_spots, _business, _start, _end);
            SetupView();
        }

        private void btn_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;
            _dc.CreateEmployee(_name, _email, _function, _business, _plate);
            SetupView();
        }

        #endregion ADD

        #region DELETE

        private void btn_deleteVisitor_Click(object sender, RoutedEventArgs e)
        {
            VisitorView visitorView = (VisitorView)dtg_visitors.SelectedItem;
            _dc.DeleteVisitor((int)visitorView.Id);
            SetupView();
        }

        private void btn_deleteContract_Click(object sender, RoutedEventArgs e)
        {
            ContractView contractView = (ContractView)dtg_contracts.SelectedItem;
            _dc.DeleteContract((int)contractView.Id);
            SetupView();
        }

        private void btn_deleteBusiness_Click(object sender, RoutedEventArgs e)
        {
            BusinessView businessView = (BusinessView)dtg_businesses.SelectedItem;
            _dc.DeleteBusiness((int)businessView.Id);
            SetupView();
        }

        private void btn_deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeView employeeView = (EmployeeView)dtg_employees.SelectedItem;
            _dc.DeleteEmployee((int)employeeView.Id);
            SetupView();
        }

        #endregion DELETE
    }
}