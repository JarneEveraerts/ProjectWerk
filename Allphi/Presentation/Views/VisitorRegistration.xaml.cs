using Domain;
using Domain.Models;
using MaterialDesignThemes.Wpf;
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

using MaterialDesignThemes.Wpf;

using MaterialDesignColors.Recommended;
using System.ComponentModel;

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
        private List<ParkingSpotView>? parkingSpotViews = new();
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
            if (_dc.GetParkingSpots().Count != 0)
            {
                foreach (var item in _dc.GetParkingSpots())
                {
                    parkingSpotViews.Add(new ParkingSpotView(item));
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
                if (_visitorPlate == "") _dc.CreateVisitor(_visitorName, _visitorEmail, _organisation, _employeeName, _businessName);
                else _dc.CreateVisitorWithPlate(_visitorName, _visitorEmail, _visitorPlate, _organisation, _employeeName, _businessName);
            }
        }

        private bool isVisitorValid(string visitorName, string visitorEmail, string visitorPlate, string organisation)
        {
            if (cmb_business.SelectedIndex == -1 || cmb_business.SelectedIndex == -1 || visitorName == "" || visitorEmail == "" || organisation == "") MessageBox.Show("Please fill in all required fields");
            else if (!_dc.IsEmailValid(visitorEmail)) { MessageBox.Show("Please enter a valid email address"); }
            else if (_dc.GetVisitorByEmail(visitorEmail) != null) { MessageBox.Show("This visitor is already registered"); }
            else if (!_dc.IsLicensePlateValid(visitorPlate)) MessageBox.Show("Please enter a valid license plate");
            else if (!_dc.ParkingSpotExists(visitorPlate)) MessageBox.Show("This license plate is not registered in the ParkingSpot database");
            else return true;
            return false;
        }

        private void cmb_business_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_employees.SelectedIndex == -1 || cmb_business.SelectedIndex != 0)
            {
                var employeesBySelectedBusiness = _dc.GetEmployeesByBusiness(cmb_business.SelectedItem.ToString());
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
                var businessBySelectedEmployees = _dc.GetBusinessIdByEmployeeName(cmb_employees.SelectedItem.ToString());
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