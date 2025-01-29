using System;
using System.Linq;
using System.Windows;
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
            var viewModel = new GarageViewModel();
            DataContext = viewModel;

            InitializeComponent();

            VehicleList.SelectedItem = viewModel.Vehicles.FirstOrDefault();
            //var service = new VehicleService(new VehicleRepository(new Infrastructure.ApplicationDbContext()));

            //var vehicleList = Task.Run(() => service.GetAll()).Result;
            //var vehicleObservable = new ObservableCollection<Vehicle>(vehicleList);
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AddVehicle_Click");
        }

        private void AddOption_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AddOption_Click");
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Apply_Click");
        }
    }
}
