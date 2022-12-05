﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using Domain;
using Persistance.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Presentation;
using Presentation.Views;
using System.Net.Http;
using Domain.Repositories;

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
            services.AddHttpClient();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IParkingSpotRepository, ParkingSpotRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<DomainController>();
            services.AddScoped<DbInitializer>();
            services.AddSingleton<ParkingApp>();
            services.AddSingleton<BalieApp>();
            services.AddSingleton<LoginAdmin>();
            services.AddSingleton<VisitorRegistration>();
            services.AddSingleton<UitgangApp>();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            //DbInitializer dbInitializer = _serviceProvider.GetService<DbInitializer>();
            //dbInitializer.Initialize();
            ParkingApp parkingApp = _serviceProvider.GetRequiredService<ParkingApp>();
            //BalieApp balieApp = _serviceProvider.GetRequiredService<BalieApp>();
            parkingApp.Show();
            //balieApp.Show();
            //LoginAdmin login = _serviceProvider.GetRequiredService<LoginAdmin>();
            //login.Show();
            //VisitorRegistration visitorRegistration = _serviceProvider.GetRequiredService<VisitorRegistration>();
            //visitorRegistration.Show();
            //UitgangApp uitgangApp = _serviceProvider.GetRequiredService<UitgangApp>();
            //uitgangApp.Show();
        }
    }
}