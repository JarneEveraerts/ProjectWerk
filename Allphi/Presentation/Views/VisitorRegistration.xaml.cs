using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for VisitorRegistration.xaml
    /// </summary>
    public partial class VisitorRegistration : Window
    {
        private DomainController _dc;
        private List<BusinessView>? businessViews = new();
        private List<EmployeeView>? employeeViews = new();
        private List<Business> _businesses = new List<Business>();
        private List<Employee> _employees = new List<Employee>();
        private HttpClient _apiClient;

        public VisitorRegistration(DomainController dc, IHttpClientFactory clientFactory)
        {
            InitializeComponent();
            _dc = dc;

            _apiClient = clientFactory.CreateClient();
            _apiClient.BaseAddress = new Uri("http://localhost:5269");
            var businessesResponse = _apiClient.GetAsync("/businesses").Result;
            var businessContentString = businessesResponse.Content.ReadAsStringAsync().Result;
            var businesses = JsonConvert.DeserializeObject<List<Business>>(businessContentString);
            _businesses = businesses; ;
            var employeesResponse = _apiClient.GetAsync("/employees").Result;
            var employeesContentString = employeesResponse.Content.ReadAsStringAsync().Result;
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesContentString);
            _employees = employees;
            if (businesses.Count != 0)
            {
                foreach (var item in businesses)
                {
                    businessViews.Add(new BusinessView(item));
                    cmb_business.Items.Add(item.Name);
                }
            }
            if (employees.Count != 0)
            {
                foreach (var item in employees)
                {
                    employeeViews.Add(new EmployeeView(item));
                    cmb_employees.Items.Add(item.Name);
                }
            }
        }

        private async void btn_Registreren_Click(object sender, RoutedEventArgs e)
        {
            string _visitorName = txt_name.Text;
            string _visitorEmail = txt_email.Text;
            string _visitorPlate = txt_plate.Text;
            string _organisation = txt_organisation.Text;

            if (await isVisitorValid(_visitorName, _visitorEmail, _visitorPlate, _organisation))
            {
                string _businessName = cmb_business.SelectedItem.ToString();
                string _employeeName = cmb_employees.SelectedItem.ToString();
                var createVisitorDTO = new CreateVisitorDTO
                {
                    Name = _visitorName,
                    Email = _visitorEmail,
                    Organisation = _organisation

                };

                //GET Business
                var business = _businesses.Single(b => b.Name == _businessName);
                //GET Employee
                var employee = _employees.Single(e => e.Name == _employeeName);

                if (_visitorPlate == "")
                {
                    //CREATE Visitor
                    var bodyString = JsonConvert.SerializeObject(createVisitorDTO);
                    var visitorResponse = await _apiClient.PutAsync("/visitors", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                    var visitorContentString = visitorResponse.Content.ReadAsStringAsync().Result;
                    var visitor = JsonConvert.DeserializeObject<Visitor>(visitorContentString);

                    //CREATE Visit
                    var createVisit = new CreateVisitDTO
                    {
                        Visitor = visitor,
                        Business = business,
                        Employee = employee
                    };
                    await CreateVisit(createVisit);
                }
                else
                {
                    var createVisitorWithPlateDTO = new CreateVisitorWithPlateDTO
                    {
                        Name = _visitorName,
                        Email = _visitorEmail,
                        Plate = _visitorPlate,
                        Organisation = _organisation,
                        Employee = employee,
                        Business = business
                    };
                    //CREATE Visitor with Plate
                    var bodyString = JsonConvert.SerializeObject(createVisitorWithPlateDTO);
                    var visitorResponse = await _apiClient.PutAsync("/visitors", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                    var visitorContentString = visitorResponse.Content.ReadAsStringAsync().Result;
                    var visitor = JsonConvert.DeserializeObject<Visitor>(visitorContentString);

                    //CREATE Visit
                    var createVisit = new CreateVisitDTO
                    {
                        Visitor = visitor,
                        Business = business,
                        Employee = employee
                    };
                    await CreateVisit(createVisit);
                };

            }
        }

        private async Task CreateVisit(CreateVisitDTO visitDTO)
        {
            var bodyString = JsonConvert.SerializeObject(visitDTO);
            await _apiClient.PutAsync("/visits", new StringContent(bodyString, Encoding.UTF8, "application/json"));

        }
        private async Task<bool> isVisitorValid(string visitorName, string visitorEmail, string visitorPlate, string organisation)
        {
            var visitorResponse = await _apiClient.GetAsync($"/visitors/email/{visitorEmail}");
            var visitorContentString = visitorResponse.Content.ReadAsStringAsync().Result;
            var visitor = JsonConvert.DeserializeObject<Visitor>(visitorContentString);

            var parkingSpotResponse = await _apiClient.GetAsync($"/parkingspots/{visitorPlate}/exists");
            var parkingSpotContentString = parkingSpotResponse.Content.ReadAsStringAsync().Result;
            var spotExists = JsonConvert.DeserializeObject<bool>(parkingSpotContentString);

            if (cmb_business.SelectedIndex == -1 || cmb_business.SelectedIndex == -1 || visitorName == "" || visitorEmail == "" || organisation == "") MessageBox.Show("Please fill in all required fields");
            else if (!_dc.IsEmailValid(visitorEmail)) { MessageBox.Show("Please enter a valid email address"); }
            else if (visitor != null) { MessageBox.Show("This visitor is already registered"); }
            else if (!_dc.IsLicensePlateValid(visitorPlate)) MessageBox.Show("Please enter a valid license plate");
            else if (!spotExists) MessageBox.Show("This license plate is not registered in the ParkingSpot database");
            else return true;
            return false;
        }

        private async void cmb_business_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_employees.SelectedIndex == -1 || cmb_business.SelectedIndex != 0)
            {

                var employeesBySelectedBusiness = _employees.Where(e => e.Business.Name == cmb_business.SelectedItem).ToList();
                cmb_employees.Items.Clear();
                foreach (var item in employeesBySelectedBusiness)
                {
                    cmb_employees.Items.Add(item.Name);
                }
            }
        }

        private async void cmb_employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_business.SelectedIndex == -1 || cmb_employees.SelectedIndex != -1)
            {
                var businessId = _employees.Single(e => e.Name == cmb_employees.SelectedItem.ToString()).Business.Id;
                var business = _businesses.Single(b => b.Id == businessId);
                cmb_business.SelectedItem = business.Name;
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