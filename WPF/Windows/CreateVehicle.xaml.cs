using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationCore.Interfaces.Services;
using WPF.ViewModels.Entities;
using WPF.ViewModels.Windows;

namespace WPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour CreateVehicle.xaml
    /// </summary>
    public partial class CreateVehicle : Window
    {
        public CreateVehicle(IList<VehicleViewModel> vehiclesViewModels, IVehicleService vehicleService, IOptionService optionService, IChassisService chassisService)
        {
            var viewModel = new CreateVehicleViewModel(vehiclesViewModels, vehicleService, optionService, chassisService, this);
            DataContext = viewModel;

            InitializeComponent();

            ChassisDataGrid.SelectedItem = viewModel.Chassis.FirstOrDefault();
        }
    }
}
