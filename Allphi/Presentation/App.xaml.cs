using System;
using System.IO;
using System.Windows;
using Domain.Services;
using Domain;
using Persistance.Data.Repositories;
using Domain.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //setup dependencyInjector
        private IServiceProvider _serviceProvider;

        private IConfiguration _configuration;

        public App()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<AllphiContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DbConnection"));
            });
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IParkingSpotRepository, ParkingSpotRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<DomainController>();
            services.AddSingleton<ParkingApp>();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            ParkingApp parkingApp = _serviceProvider.GetRequiredService<ParkingApp>();
            parkingApp.Show();
        }
    }
}