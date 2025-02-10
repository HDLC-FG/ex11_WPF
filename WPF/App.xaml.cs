using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using WPF.ViewModels;

namespace WPF
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-FR");

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ApplicationDbContext>((provider) =>
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GarageConnection"].ConnectionString);
                connection.Open();
                return new ApplicationDbContext(connection, connectionDisposeWithContext: true);
            });

            serviceCollection.AddSingleton<GarageWindow>();
            serviceCollection.AddSingleton<IGarageViewModel, GarageViewModel>();
            serviceCollection.AddScoped<IVehicleService, VehicleService>();
            serviceCollection.AddScoped<IChassisService, ChassisService>();
            serviceCollection.AddScoped<IOptionService, OptionService>();
            serviceCollection.AddScoped<IVehicleRepository, VehicleRepository>();
            serviceCollection.AddScoped<IChassisRepository, ChassisRepository>();
            serviceCollection.AddScoped<IOptionRepository, OptionRepository>();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var garageWindow = ServiceProvider.GetRequiredService<GarageWindow>();
            garageWindow.Show();
        }
    }
}
