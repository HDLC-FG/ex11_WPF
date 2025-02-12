using System;
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
        private readonly CreateVehicleViewModel viewModel;

        public CreateVehicle(IList<VehicleViewModel> vehiclesViewModels, IVehicleService vehicleService, IOptionService optionService, IChassisService chassisService)
        {
            viewModel = new CreateVehicleViewModel(vehiclesViewModels, vehicleService, optionService, chassisService, this);
            DataContext = viewModel;

            InitializeComponent();

            ChassisDataGrid.SelectedItem = viewModel.Chassis.FirstOrDefault();
        }

        protected override void OnClosed(EventArgs e)
        {
            viewModel.Dispose();
            base.OnClosed(e);
        }
    }
}
