using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Newtonsoft.Json;
using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

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
        private HttpClient _apiClient;
        private List<Business> _businesses = new List<Business>();
        private List<Visitor> _visitors = new List<Visitor>();
        private List<Employee> _employees = new List<Employee>();
        private List<Contract> _contracts = new List<Contract>();
        private List<Visit> _visits = new List<Visit>();
        private List<ParkingSpot> _parkingSpots = new List<ParkingSpot>();

        public BalieApp(DomainController dc, IHttpClientFactory clientFactory)
        {
            _dc = dc;
            _apiClient = clientFactory.CreateClient();
            _apiClient.BaseAddress = new Uri("http://localhost:5076");
            _apiClient.DefaultRequestHeaders
      .Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            //raise
            SetupView();
        }

        private async void SetupView()
        {
            _businessViews = await GetBusinessViews();
            _contractViews = await GetContractViews();
            _employeeViews = await GetEmployeeViews();
            _parkingSpotViews = await GetParkingSpotViews();
            _visitorViews = await GetVisitorViews();
            _visitViews = await GetVisitViews();
            dtg_businesses.ItemsSource = _businessViews;
            dtg_contracts.ItemsSource = _contractViews;
            dtg_employees.ItemsSource = _employeeViews;
            dtg_visitors.ItemsSource = _visitorViews;
            dtg_visits.ItemsSource = _visitViews;
        }

        #region SelectionChange

        private async void cmb_business_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_employees.SelectedIndex == -1 || cmb_business.SelectedIndex != 0)
            {

                var employeesBySelectedBusinessRespons = await _apiClient.GetAsync($"/employees/business/{cmb_business.SelectedItem.ToString()}");
                var employeesBySelectedBusinessContentString = await employeesBySelectedBusinessRespons.Content.ReadAsStringAsync();
                var employeesBySelectedBusiness = JsonConvert.DeserializeObject<List<Employee>>(employeesBySelectedBusinessContentString);
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
                var employeeResponse = await _apiClient.GetAsync($"/employees/{cmb_employees.SelectedItem.ToString()}/businessId");
                var contentStringEmployee = await employeeResponse.Content.ReadAsStringAsync();
                var businessId = JsonConvert.DeserializeObject<int>(contentStringEmployee);

                cmb_business.SelectedItem = _businessViews.Single(b => b.Id == businessId);
            }
        }

        #endregion SelectionChange

        #region GetViews

        private async Task<List<BusinessView>> GetBusinessViews()
        {
            var businessesResponse = await _apiClient.GetAsync("/businesses");
            var contentString = await businessesResponse.Content.ReadAsStringAsync();
            var businesses = JsonConvert.DeserializeObject<List<Business>>(contentString);
            _businesses = businesses;
            List<BusinessView> businessViews = new();
            foreach (var item in businesses)
            {
                businessViews.Add(new BusinessView(item));
                cmb_business.Items.Add(item.Name);
            }
            return businessViews;
        }

        private async Task<List<ContractView>> GetContractViews()
        {
            var contractsResponse = await _apiClient.GetAsync("/contracts");
            var contentString = await contractsResponse.Content.ReadAsStringAsync();
            var contracts = JsonConvert.DeserializeObject<List<Contract>>(contentString);
            _contracts = contracts;
            List<ContractView> contractViews = new();
            foreach (var item in contracts)
            {
                contractViews.Add(new ContractView(item));
            }
            return contractViews;
        }

        private async Task<List<EmployeeView>> GetEmployeeViews()
        {
            var employeeResponse = await _apiClient.GetAsync("/employees");
            var contentString = await employeeResponse.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<Employee>>(contentString);
            _employees = employees;
            List<EmployeeView> employeeViews = new();
            foreach (var item in employees)
            {
                employeeViews.Add(new EmployeeView(item));
                cmb_employees.Items.Add(item.Name);
            }
            return employeeViews;
        }

        private async Task<List<ParkingSpotView>> GetParkingSpotViews()
        {
            var parkingSpotResponse = await _apiClient.GetAsync("/parkingspots");
            var contentString = await parkingSpotResponse.Content.ReadAsStringAsync();
            var parkingSpots = JsonConvert.DeserializeObject<List<ParkingSpot>>(contentString);
            _parkingSpots = parkingSpots;
            List<ParkingSpotView> parkingSpotViews = new();
            foreach (var item in parkingSpots)
            {
                parkingSpotViews.Add(new ParkingSpotView(item));
            }
            return parkingSpotViews;
        }

        private async Task<List<VisitorView>> GetVisitorViews()
        {
            var visitorResponse = await _apiClient.GetAsync("/Visitors");
            var contentString = await visitorResponse.Content.ReadAsStringAsync();
            var visitors = JsonConvert.DeserializeObject<List<Visitor>>(contentString);
            _visitors = visitors;
            List<VisitorView> visitorViews = new();
            foreach (var item in visitors)
            {
                visitorViews.Add(new VisitorView(item));
            }
            return visitorViews;
        }

        private async Task<List<VisitView>> GetVisitViews()
        {
            var visitRespons = await _apiClient.GetAsync("/Visits");
            var contentString = await visitRespons.Content.ReadAsStringAsync();
            var visits = JsonConvert.DeserializeObject<List<Visit>>(contentString);
            _visits = visits;
            List<VisitView> visitViews = new();
            foreach (var item in visits)
            {
                visitViews.Add(new VisitView(item));
            }
            return visitViews;
        }

        #endregion GetViews

        #region UPDATE

        private async void btn_updateBusiness_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_business_name.Text;
            string? _address = txt_business_address.Text;
            string? _phone = txt_business_phone.Text;
            string _email = txt_business_email.Text;
            string _btw = txt_business_btw.Text;

            if (IsBusinessValid(_name, _address, _phone, _email, _btw))
            {
                BusinessView businessView = (BusinessView)dtg_businesses.SelectedItem;

                var updateBusiness = new CreateAndUpdateBusinessDTO
                {
                    Name = _name,
                    Address = _address,
                    Phone = _phone,
                    Email = _email,
                    Btw = _btw
                };
                var bodyString = JsonConvert.SerializeObject(updateBusiness);
                var businessesResponse = await _apiClient.PatchAsync($"/businesses/{(int)businessView.Id}", new StringContent(bodyString, Encoding.UTF8,
                                    "application/json"));
                if (businessesResponse.IsSuccessStatusCode)
                    SetupView();
                else
                    throw new Exception();
            }
        }

        private async void btn_updateVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;

            if (IsVisitorValid(_name, _email, _plate, _business))
            {
                VisitorView visitorView = (VisitorView)dtg_visitors.SelectedItem;
                var updateVisitorDTO = new UpdateVisitorDTO
                {
                    Name = _name,
                    Email = _email,
                    Plate = _plate,
                    Business = _business
                };
                var bodyString = JsonConvert.SerializeObject(updateVisitorDTO);
                var response = await _apiClient.PatchAsync($"/visitors/{(int)visitorView.Id}", new StringContent(bodyString, Encoding.UTF8,
                                    "application/json"));
                if (response.IsSuccessStatusCode)
                    SetupView();
                else
                    throw new Exception();
            }
        }

        private async void btn_updateContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            if (IsContractValid(_spots, _business, _start))
            {
                var business = _businesses.Single(b => b.Name == _business);
                ContractView contractView = (ContractView)dtg_contracts.SelectedItem;
                var updateContract = new UpdateContractDTO
                {
                    Start = _start,
                    End = _end,
                    Spots = Int32.Parse(_spots),
                    Business = business
                };

                var bodyString = JsonConvert.SerializeObject(updateContract);
                var response = await _apiClient.PatchAsync($"/contracts/{(int)contractView.Id}", new StringContent(bodyString, Encoding.UTF8,
                                    "application/json"));
                if (response.IsSuccessStatusCode)
                    SetupView();
                else
                    throw new Exception();
            }
        }

        private async void btn_updateEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;

            var business = _businesses.Single(b => b.Name == _business);

            if (IsEmployeeValid(_name, _email, _function, _business, _plate))
            {
                EmployeeView employeeView = (EmployeeView)dtg_employees.SelectedItem;

                var updateEmployee = new CreateAndUpdateEmployeeDTO
                {
                    Name = _name,
                    Email = _email,
                    Function = _function,
                    Business = business,
                    Plate = _plate
                };

                var bodyString = JsonConvert.SerializeObject(updateEmployee);
                var employeeResponse = await _apiClient.PatchAsync($"/employees/{(int)employeeView.Id}", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                if (employeeResponse.IsSuccessStatusCode)
                {
                    SetupView();

                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private async void btn_updateVisit_Click(object sender, RoutedEventArgs e)
        {
            string name = txt_visit_name.Text;
            var visitor = _visitors.Single(v => v.Name == name);
            if (IsVisitValid(name, visitor))
            {
                MessageBox.Show("Ben je zeker om deze bezoek te updaten?", "Update", MessageBoxButton.OKCancel);

                var employee = _employees.Single(e => e.Name == cmb_business.Text);
                var business = _businesses.Single(b => b.Name == cmb_business.Text);
                var updateVisit = new UpdateVisitDTO
                {
                    Visitor = visitor,
                    Employee = employee,
                    Business = business,
                    Start = dtp_visit_start.SelectedDate.Value,
                    End = dtp_visit_end.SelectedDate.Value
                };
                var bodyString = JsonConvert.SerializeObject(updateVisit);
                var updateVisitResponse = await _apiClient.PatchAsync($"/visits", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                if (updateVisitResponse.IsSuccessStatusCode)
                {
                    SetupView();

                }
                else
                {
                    throw new Exception();
                }
            }
        }

        #endregion UPDATE

        #region ADD

        private async void btn_addBusiness_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_business_name.Text;
            string? _address = txt_business_address.Text;
            string? _phone = txt_business_phone.Text;
            string _email = txt_business_email.Text;
            string _btw = txt_business_btw.Text;
            if (IsBusinessValid(_name, _address, _phone, _email, _btw))
            {

                var createBusiness = new CreateAndUpdateBusinessDTO
                {
                    Name = _name,
                    Address = _address,
                    Phone = _phone,
                    Email = _email,
                    Btw = _btw
                };
                var bodyString = JsonConvert.SerializeObject(createBusiness);
                var businessesResponse = await _apiClient.PutAsync($"/businesses", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                if (businessesResponse.IsSuccessStatusCode)
                    SetupView();
                else
                {
                    var exception = businessesResponse.Content.ReadAsStringAsync().Result;
                    MessageBox.Show(exception);
                }
                SetupView();
            }
        }

        private async void btn_addVisitor_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_visitor_name.Text;
            string _email = txt_visitor_email.Text;
            string? _plate = txt_visitor_plate.Text;
            string _business = txt_visitor_business.Text;

            if (IsVisitorValid(_name, _email, _plate, _business))
            {
                var visitorExists = _visitors.Exists(v => v.Name == _name);

                if (!visitorExists)
                {
                    var createVisitor = new CreateVisitorBalieDTO
                    {
                        Name = _name,
                        Email = _email,
                        Plate = _plate,
                        Business = _business
                    };
                    var bodyString = JsonConvert.SerializeObject(createVisitor);
                    var visitorResponse = await _apiClient.PutAsync("/visitors/balie", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                    if (visitorResponse.IsSuccessStatusCode)
                        SetupView();
                    else
                        MessageBox.Show("Er is iets fout gegaan");
                }
                else
                {
                    MessageBox.Show("Bezoeker bestaat al");
                }
            }
        }

        private async void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            DateTime _start = dtp_start.SelectedDate.Value;
            DateTime _end = dtp_end.SelectedDate.Value;
            string _spots = txt_contract_spots.Text;
            string _business = txt_contract_business.Text;
            if (IsContractValid(_spots, _business, _start))
            {

                var business = _businesses.SingleOrDefault(b => b.Name == _business);
                if (business == null)
                {
                    MessageBox.Show("Het opgegeven bedrijf staat niet in ons systeem.");
                    return;
                }
                var contractExists = _contracts.Exists(c => c.Business.Id == business.Id);
                if (!contractExists)
                {
                    var createContractDTO = new CreateContractDTO
                    {
                        StartDate = _start,
                        EndDate = _end,
                        TotalSpaces = int.Parse(_spots),
                        Business = business
                    };

                    var bodyString = JsonConvert.SerializeObject(createContractDTO);
                    var contractResponse = await _apiClient.PutAsync($"/contracts", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                    if (contractResponse.IsSuccessStatusCode)
                        SetupView();
                    else
                    {
                        var exception = contractResponse.Content.ReadAsStringAsync().Result;
                        MessageBox.Show(exception);
                    }
                    SetupView();
                }
            }
        }

        private async void btn_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string _name = txt_employee_name.Text;
            string? _email = txt_employee_email.Text;
            string _function = txt_employee_function.Text;
            string _business = txt_employee_business.Text;
            string? _plate = txt_employee_plate.Text;

            if (IsEmployeeValid(_name, _email, _function, _business, _plate))
            {
                var employeeExists = _employees.Exists(e => e.Name == _name);
                if (!employeeExists)
                {
                    var business = _businesses.Single(b => b.Name == _business);
                    var CreateEmployee = new CreateAndUpdateEmployeeDTO
                    {
                        Name = _name,
                        Email = _email,
                        Function = _function,
                        Business = business,
                        Plate = _plate
                    };
                    var bodyString = JsonConvert.SerializeObject(CreateEmployee);
                    var employeeResponse = await _apiClient.PutAsync($"/employees", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                    if (employeeResponse.IsSuccessStatusCode)
                    {
                        SetupView();
                    }
                    else
                    {
                        var exception = employeeResponse.Content.ReadAsStringAsync().Result;
                        MessageBox.Show(exception);
                    }
                    SetupView();
                }
                else
                {
                    MessageBox.Show("Medewerker bestaat al");
                }
            }
        }

        private async void btn_addVisit_Click(object sender, RoutedEventArgs e)
        {
            string name = txt_visit_name.Text;
            if (IsVisitValid(name))
            {
                var visitExists = _visits.Exists(v => v.Visitor.Name == name);
                if (!visitExists)
                {
                    var visitor = _visitors.Single(v => v.Name == name);
                    var createVisitDTO = new CreateVisitDTO
                    {
                        Visitor = visitor,
                        Employee = _employees.First(e => e.Name == cmb_employees.Text),
                        Business = _businesses.First(b => b.Name == cmb_business.Text),
                    };
                    var bodyString = JsonConvert.SerializeObject(createVisitDTO);
                    var employeeResponse = await _apiClient.PutAsync($"/visits", new StringContent(bodyString, Encoding.UTF8, "application/json"));
                    if (employeeResponse.IsSuccessStatusCode)
                    {
                        SetupView();
                    }
                    else
                    {
                        MessageBox.Show("Er is iets fout gegaan");
                    }
                }
                else
                {
                    MessageBox.Show("Bezoek bestaat al");
                }
            }
        }

        #endregion ADD

        #region DELETE

        private async void btn_deleteVisitor_Click(object sender, RoutedEventArgs e)
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
                    var visitorResponse = await _apiClient.DeleteAsync($"/visitors/{(int)visitorView.Id}");
                    SetupView();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured.");
            }
        }

        private async void btn_deleteContract_Click(object sender, RoutedEventArgs e)
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
                    var contractResponse = await _apiClient.DeleteAsync($"/contracts/{(int)contractView.Id}");
                    SetupView();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured.");
            }
        }

        private async void btn_deleteBusiness_Click(object sender, RoutedEventArgs e)
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

                    var businessesResponse = await _apiClient.DeleteAsync($"/businesses/{(int)businessView.Id}");
                    SetupView();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured.");
            }
        }

        private async void btn_deleteEmployee_Click(object sender, RoutedEventArgs e)
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
                    var emloyeeResponse = await _apiClient.DeleteAsync($"/employees/{(int)employeeView.Id}");
                    SetupView();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured.");
            }
        }

        private async void btn_deleteVisit_Click(object sender, RoutedEventArgs e)
        {
            string name = txt_visit_name.Text;
            if (IsVisitValid(name))
            {
                MessageBox.Show("Ben je zeker om deze bezoek te verwijderen?", "Delete", MessageBoxButton.OKCancel);

                VisitView visitView = (VisitView)dtg_employees.SelectedItem;
                var visitResponse = await _apiClient.DeleteAsync($"/visits/{(int)visitView.Id}");
                SetupView();
            }
        }

        #endregion DELETE

        #region Validation

        private bool IsBusinessValid(string name, string address, string phone, string email, string btw)
        {
            if (!_dc.IsEmailValid(email)) MessageBox.Show("Email is niet geldig");
            else if (!_dc.IsBtwValid(btw)) MessageBox.Show("BTW nummer is niet geldug.");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (string.IsNullOrEmpty(address)) MessageBox.Show("Adres is leeg");
            else if (string.IsNullOrEmpty(phone)) MessageBox.Show("Telefoonnummer is leeg");
            else if (string.IsNullOrEmpty(btw)) MessageBox.Show("BTW is leeg");
            else if (string.IsNullOrEmpty(email)) MessageBox.Show("Email is leeg");
            else return true;
            return false;
        }

        private bool IsContractValid(string spots, string business, DateTime? start)
        {
            if (string.IsNullOrEmpty(spots)) MessageBox.Show("Aantal parkeerplaatsen is niet geldig");
            else if (string.IsNullOrEmpty(business)) MessageBox.Show("Bedrijfsnaam is niet geldig");
            else if (start == null) MessageBox.Show("Start datum mag niet leeg zijn");
            else return true;
            return false;
        }

        private bool IsEmployeeValid(string name, string email, string plate, string business, string function)
        {
            if (!_dc.IsEmailValid(email)) MessageBox.Show("Email is niet geldig");
            else if (IsLicensePlateValid(plate)) MessageBox.Show("BTW nummer is niet geldig.");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (string.IsNullOrEmpty(business)) MessageBox.Show("Bedrijfsnaam is leeg");
            else if (string.IsNullOrEmpty(email)) MessageBox.Show("Email is leeg");
            else if (string.IsNullOrEmpty(function)) MessageBox.Show("Functie is leeg");
            else return true;
            return false;
        }

        private bool IsVisitorValid(string name, string email, string plate, string business)
        {
            if (!_dc.IsEmailValid(email)) MessageBox.Show("Email is niet geldig");
            else if (IsLicensePlateValid(plate)) MessageBox.Show("nummer plaat is niet geldig.");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (string.IsNullOrEmpty(business)) MessageBox.Show("Bedrijfsnaam is leeg");
            else if (string.IsNullOrEmpty(email)) MessageBox.Show("Email is leeg");
            else return true;
            return false;
        }

        private bool IsLicensePlateValid(string plate)
        {
            if (plate != "")
            {
                if (!_dc.IsLicensePlateValid(plate))
                {
                    MessageBox.Show("Nummerplaat is niet geldig");
                    return false;
                }
            }

            return true;
        }

        private bool IsVisitValid(string name, Visitor visitor = null)
        {
            if (cmb_business.SelectedIndex == -1) MessageBox.Show("Duid een bedrijf aan");
            else if (cmb_employees.SelectedIndex == -1) MessageBox.Show("Duid een werknemer aan");
            else if (dtp_visit_start.SelectedDate == null) MessageBox.Show("Duid een start datum aan");
            else if (dtp_visit_end.SelectedDate == null) MessageBox.Show("Duid een eind datum aan");
            else if (string.IsNullOrEmpty(name)) MessageBox.Show("Naam is leeg");
            else if (visitor == null) MessageBox.Show("Visitor bestaat niet");
            else return true;
            return false;
        }

        #endregion Validation
    }
}