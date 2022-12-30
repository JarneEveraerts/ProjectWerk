using Domain;
using MaterialDesignThemes.Wpf;
using Presentation.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Shared.Dto;
using System.Net.Http;
using System;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for VisitorRegistration.xaml
    /// </summary>
    public partial class VisitorRegistration : Window
    {
        private ViewController _vc;
        private List<List<string>> businesses;

        private List<BusinessView>? businessViews = new();
        private List<VisitView>? visitViews = new();
        private List<VisitorView>? visitorViews = new();
        private List<EmployeeView>? employeeViews = new();
        private List<ParkingSpotView>? parkingSpotViews = new();
        private VisitorView Visitor;
        private HttpClient _api;

        public VisitorRegistration(ViewController vc, IHttpClientFactory clientFactory)
        {
            InitializeComponent();
            _vc = vc;
            GetBusinessViews();
            GetEmployeeViews();
        }

        private void GetEmployeeViews()
        {
            var employees = _vc.GetEmployeeViews();
            if (employees.Count != 0)
            {
                foreach (var item in employees)
                {
                    employeeViews.Add(item);
                    cmb_employees.Items.Add(item.Name);
                }
            }
        }

        private void GetBusinessViews()
        {
            var businesses = _vc.GetBusinessViews();
            if (businesses.Count != 0)
            {
                foreach (var item in businesses)
                {
                    businessViews.Add(item);
                    cmb_business.Items.Add(item.Name);
                }
            }
        }

        private void btn_Registreren_Click(object sender, RoutedEventArgs e)
        {
            string _visitorName = txt_name.Text;
            string _visitorEmail = txt_email.Text;
            string _visitorPlate = txt_plate.Text;
            string _organisation = txt_organisation.Text;

            if (isVisitorValid(_visitorName, _visitorEmail, _visitorPlate, _organisation))
            {
                string _businessName = cmb_business.SelectedItem.ToString();
                string _employeeName = cmb_employees.SelectedItem.ToString();
                if (_visitorPlate == "") _vc.CreateVisitor(_visitorName, _visitorEmail, _organisation, _employeeName, _businessName);
                else _vc.CreateVisitorWithPlate(_visitorName, _visitorEmail, _visitorPlate, _organisation, _employeeName, _businessName);
            }
        }

        private bool isVisitorValid(string visitorName, string visitorEmail, string visitorPlate, string organisation)
        {
            if (visitorPlate != "")
            {
                if (!_vc.IsLicensePlateValid(visitorPlate))
                {
                    MessageBox.Show("Please enter a valid license plate");
                    return false;
                }
                else if (!_vc.ParkingSpotExists(visitorPlate))
                {
                    MessageBox.Show("This license plate is not registered in the ParkingSpot database");
                    return false;
                }
            }
            if (cmb_business.SelectedIndex == -1 || cmb_business.SelectedIndex == -1 || visitorName == "" || visitorEmail == "" || organisation == "") MessageBox.Show("Please fill in all required fields");
            else if (!_vc.IsEmailValid(visitorEmail)) { MessageBox.Show("Please enter a valid email address"); }
            else if (_vc.GetVisitorByEmail(visitorEmail) != null) { MessageBox.Show("This visitor is already registered"); }
            else return true;
            return false;
        }

        private void cmb_business_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_employees.SelectedIndex == -1 || cmb_business.SelectedIndex != 0)
            {
                var employeesBySelectedBusiness = _vc.GetEmployeesByBusiness(cmb_business.SelectedItem.ToString());
                cmb_employees.Items.Clear();
                foreach (var item in employeesBySelectedBusiness)
                {
                    cmb_employees.Items.Add(item.Name);
                }
            }
        }

        private void cmb_employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_business.SelectedIndex == -1 || cmb_employees.SelectedIndex != -1)
            {
                var businessBySelectedEmployees = _vc.GetBusinessIdByEmployeeName(cmb_employees.SelectedItem.ToString());
                cmb_business.SelectedItem = businessBySelectedEmployees.Name;
            }
        }

        private void Btn_NL_Click(object sender, RoutedEventArgs e)
        {
            lbl_Header.Content = "Welkom";
            HintAssist.SetHint(txt_name, "Naam*");
            HintAssist.SetHint(txt_email, "Email*");
            HintAssist.SetHint(txt_organisation, "Organisatie*");
            HintAssist.SetHint(txt_plate, "Nummerplaat");
            HintAssist.SetHint(cmb_employees, "Contactpersoon*");
            HintAssist.SetHint(cmb_business, "Bedrijf*");
            btn_Registreren.Content = "Registreren";
        }

        private void Btn_FR_Click(object sender, RoutedEventArgs e)
        {
            lbl_Header.Content = "Bonjour";
            HintAssist.SetHint(txt_name, "Nom*");
            HintAssist.SetHint(txt_email, "E-mail*");
            HintAssist.SetHint(txt_organisation, "Organisation*");
            HintAssist.SetHint(txt_plate, "Plaque d'immatriculation");
            HintAssist.SetHint(cmb_employees, "Contact*");
            HintAssist.SetHint(cmb_business, "Compagnie*");
            btn_Registreren.Content = "Régistré";
        }

        private void Btn_ENG_Click(object sender, RoutedEventArgs e)
        {
            lbl_Header.Content = "Welcome";
            HintAssist.SetHint(txt_name, "Name");
            HintAssist.SetHint(txt_email, "Email");
            HintAssist.SetHint(txt_organisation, "Organisation*");
            HintAssist.SetHint(txt_plate, "License plate");
            HintAssist.SetHint(cmb_employees, "Contact person*");
            HintAssist.SetHint(cmb_business, "Business*");
            btn_Registreren.Content = "Register";
        }
    }
}