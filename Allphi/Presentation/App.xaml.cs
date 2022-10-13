using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Domain.Services;
using Domain;
using Persistance.Data.Repositories;
using Domain.View;
using Persistance.Data;
using Persistance.Data.Configuration;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Services.Configurator = new Configurator(e.Args);
            IBusinessRepository businessRepo = new BusinessRepository();
            IContractRepository contractRepo = new ContractRepository();
            IEmployeeRepository employeeRepo = new EmployeeRepository();
            IParkingRepository parkingRepo = new ParkingRepository();
            IVisitorRepository visitorRepo = new VisitorRepository();
            IVisitRepository visitRepo = new VisitRepository();

            Domaincontroller dc = new Domaincontroller(businessRepo, contractRepo, employeeRepo, parkingRepo, visitorRepo, visitRepo);

            ParkingApp parkingApp = new(dc);
            parkingApp.Show();
        }
    }
}