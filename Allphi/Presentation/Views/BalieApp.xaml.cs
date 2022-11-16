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

            if (IsBusinessValid(_name, _address, _phone, _email, _btw))
            {

                BusinessView businessView = (BusinessView)dtg_businesses.SelectedItem;
                _dc.UpdateBusiness(_name, _address, _phone, _email, _btw, (int)businessView.Id);
                SetupView();

            }
        }

        private void btn_updateVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;

            if (IsVisitorValid(_name, _email, _plate, _business))
            {

                VisitorView visitorView = (VisitorView)dtg_visitors.SelectedItem;
                _dc.UpdateVisitor(_name, _email, _plate, _business, (int)visitorView.Id);
                SetupView();

            }
        }

        private void btn_updateContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = lbl_contract_spots.Content.ToString();
            string _business = txt_contract_business.Text;
            if (IsContractValid(_spots, _business, _start))
            {
                ContractView contractView = (ContractView)dtg_contracts.SelectedItem;
                _dc.UpdateContract(_spots, _business, _start, _end, (int)contractView.Id);
                SetupView();
            }
        }

        private void btn_updateEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;
            if (IsEmployeeValid(_name, _email, _function, _business, _plate))
            {

                EmployeeView employeeView = (EmployeeView)dtg_employees.SelectedItem;
                _dc.UpdateEmployee(_name, _email, _function, _business, _plate, (int)employeeView.Id);
                SetupView();

            }
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
            if (IsBusinessValid(_name, _address, _phone, _email, _btw))
            {
                _dc.CreateBusiness(_name, _address, _phone, _email, _btw);
                SetupView();
            }
        }

        private void btn_addVisitor_Click(object sender, RoutedEventArgs e)
        {

            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;

            if (IsVisitorValid(_name, _email, _plate, _business))
            {

                //_dc.CreateVisitor(_name, _email, _plate, _business);
                SetupView();
            }
        }

        private void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = lbl_contract_spots.Content.ToString();
            string _business = txt_contract_business.Text;
            if (IsContractValid(_spots, _business, _start))
            {
                _dc.CreateContract(_spots, _business, _start, _end);
                SetupView();
            }
        }

        private void btn_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;

            if (IsEmployeeValid(_name, _email, _function, _business, _plate))
            {

                _dc.CreateEmployee(_name, _email, _function, _business, _plate);
                SetupView();
            }
        }

        #endregion ADD

        #region DELETE

        private void btn_deleteVisitor_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dtg_visitors.SelectedItem == null)
                {
                    MessageBox.Show("Gelieve iemand te selecteren");
                }
                else
                {

                    MessageBox.Show("Ben je zeker om deze visitor te verwijderen?", "Delete", MessageBoxButton.OKCancel);
                    VisitorView visitorView = (VisitorView)dtg_visitors.SelectedItem;
                    _dc.DeleteVisitor((int)visitorView.Id);
                    SetupView();

                }

            }
            catch (Exception)
            {

                MessageBox.Show("An error occured.");
            }
        }

        private void btn_deleteContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dtg_contracts.SelectedItem == null)
                {
                    MessageBox.Show("Gelieve iets te selecteren");
                }
                else
                {


                    MessageBox.Show("Ben je zeker om dit contract te verwijderen?", "Delete", MessageBoxButton.OKCancel);


                    ContractView contractView = (ContractView)dtg_contracts.SelectedItem;
                    _dc.DeleteContract((int)contractView.Id);
                    SetupView();

                }
            }
            catch (Exception)
            {

                MessageBox.Show("An error occured.");
            }
        }

        private void btn_deleteBusiness_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dtg_businesses.SelectedItem == null)
                {
                    MessageBox.Show("Gelieve iemand te selecteren");
                }
                else
                {


                    MessageBox.Show("Ben je zeker om dit bedrijf te verwijderen?", "Delete", MessageBoxButton.OKCancel);


                    BusinessView businessView = (BusinessView)dtg_businesses.SelectedItem;
                    _dc.DeleteBusiness((int)businessView.Id);
                    SetupView();

                }
            }

            catch (Exception)
            {

                MessageBox.Show("An error occured.");
            }
        }

        private void btn_deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtg_employees.SelectedItem == null)
                {

                    MessageBox.Show("Gelieve iemand te selecteren");
                }
                else
                {

                    MessageBox.Show("Ben je zeker om deze werknemer te verwijderen?", "Delete", MessageBoxButton.OKCancel);


                    EmployeeView employeeView = (EmployeeView)dtg_employees.SelectedItem;
                    _dc.DeleteEmployee((int)employeeView.Id);
                    SetupView();

                }

            }

            catch (Exception)
            {

                MessageBox.Show("An error occured.");
            }
        }

        #endregion DELETE

        private bool IsBusinessValid(string name, string address, string phone, string email, string btw)
        {

            if (!_dc.IsEmailValid(email)) MessageBox.Show("Email is niet geldig");
            else if (!_dc.IsBtwValid(btw)) MessageBox.Show("BTW nummer is niet geldug.");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (string.IsNullOrEmpty(address)) MessageBox.Show("Adres is leeg");
            else if (string.IsNullOrEmpty(phone)) MessageBox.Show("Telefoonnummer is leeg");
            else if (string.IsNullOrEmpty(btw)) MessageBox.Show("BTW is leeg");
            else if (string.IsNullOrEmpty(email)) MessageBox.Show("Email is leeg");
            else
            {
                return true;
            }
            return false;
        }
        private bool IsContractValid(string spots, string business, DateTime? start)
        {
            if (string.IsNullOrEmpty(spots)) MessageBox.Show("Aantal parkeerplaatsen is niet geldig");
            else if (string.IsNullOrEmpty(business)) MessageBox.Show("Bedrijfsnaam is niet geldig");
            else if (start == null) MessageBox.Show("Start datum mag niet leeg zijn");
            else
            {
                return true;
            }
            return false;
        }
        private bool IsEmployeeValid(string name, string email, string plate, string business, string function)
        {

            if (!_dc.IsEmailValid(email)) MessageBox.Show("Email is niet geldig");
            else if (!_dc.IsLicensePlateValid(plate)) MessageBox.Show("BTW nummer is niet geldig.");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (string.IsNullOrEmpty(business)) MessageBox.Show("Bedrijfsnaam is leeg");
            else if (string.IsNullOrEmpty(email)) MessageBox.Show("Email is leeg");
            else if (string.IsNullOrEmpty(function)) MessageBox.Show("Functie is leeg");
            else
            {
                return true;
            }
            return false;
        }
        private bool IsVisitorValid(string name, string email, string plate, string business)
        {
            if (!_dc.IsEmailValid(email)) MessageBox.Show("Email is niet geldig");
            else if (!_dc.IsLicensePlateValid(plate)) MessageBox.Show("nummer plaat is niet geldig.");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (string.IsNullOrEmpty(business)) MessageBox.Show("Bedrijfsnaam is leeg");
            else if (string.IsNullOrEmpty(email)) MessageBox.Show("Email is leeg");
            else
            {
                return true;

            }
            return false;
        }
    }
}