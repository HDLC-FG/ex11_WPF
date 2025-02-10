using System.Linq;
using System.Windows;
using ApplicationCore.Interfaces.Services;
using WPF.ViewModels;

namespace WPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour CreateVehicle.xaml
    /// </summary>
    public partial class CreateVehicle : Window
    {
        public CreateVehicle(IVehicleService vehicleService, IOptionService optionService, IChassisService chassisService)
        {
            var viewModel = new CreateVehicleViewModel(vehicleService, optionService, chassisService, this);
            DataContext = viewModel;

            InitializeComponent();

            ChassisDataGrid.SelectedItem = viewModel.Chassis.FirstOrDefault();
        }
    }
}
