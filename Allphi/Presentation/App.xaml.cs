using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Persistance.Data;
using Domain.Services;
using Persistance.Data.Configuration;
using Domain;
using Persistance.Data.Repositorys;
using Domain.View;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {

            Services.Configurator = new Configurator(new string[] { });

            IBussinesRepository bussinesRepo = new BusinessRepository();
            IContractRepository contractRepo = new ContractRepository();
            IEmployeeRepository employeeRepo = new EmployeeRepository();
            IParkingRepository parkingRepo = new ParkingRepository();
            IVisitorRepository visitorRepo = new VisitorRepository();
            IVisitRepository visitRepo = new VisitRepository();


            Domaincontroller dc = new Domaincontroller(bussinesRepo, contractRepo,employeeRepo, parkingRepo, visitorRepo,visitRepo);

            ParkingApp scherm = new(dc);
            scherm.Show();
        }
    }

}
