using System;
using System.Linq;
using System.Windows;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Repositories;
using WPF.ViewModels;

namespace WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var viewModel = new GarageViewModel(new VehicleService(new VehicleRepository(new ApplicationDbContext())));
            DataContext = viewModel;

            InitializeComponent();

            VehicleList.SelectedItem = viewModel.Vehicles.FirstOrDefault();
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AddVehicle_Click");
        }

        private void AddOption_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AddOption_Click");
        }
    }
}
