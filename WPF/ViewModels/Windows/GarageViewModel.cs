using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using WPF.Converters;
using WPF.Events;
using WPF.ViewModels.Entities;
using WPF.Windows;
using static ApplicationCore.Enums;

namespace WPF.ViewModels.Windows
{
    public class GarageViewModel : NotifyPropertyChanged, IGarageViewModel
    {
        private readonly IVehicleService vehicleService;
        private readonly IChassisService chassisService;
        private readonly IOptionService optionService;
        private VehicleViewModel selectedVehicle;

        public GarageViewModel(IVehicleService vehicleService, IChassisService chassisService, IOptionService optionService)
        {
            this.vehicleService = vehicleService;
            this.chassisService = chassisService;
            this.optionService = optionService;

            EngineTypes = Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToList();

            var vehicles = Task.Run(() => vehicleService.GetAll()).Result;
            Vehicles = new ObservableCollection<VehicleViewModel>(vehicles.Select(v => new VehicleViewModel(v)));
        }

        public IList<EngineType> EngineTypes { get; set; }
        public ObservableCollection<VehicleViewModel> Vehicles { get; set; }
        public VehicleViewModel SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
            }
        }
        public ICommand UpdateVehicleCommand => new Command(execute => UpdateVehicle());
        public ICommand AddOptionsCommand => new Command(execute => ShowOptionWindow());
        public ICommand CreateVehicleCommand => new Command(execute => ShowCreateVehicleWindow());
        public ICommand DeleteVehicleCommand => new Command(execute => DeleteVehicle(execute), canExecute => Command.IsNotNullOrEmpty(canExecute));

        public void Dispose()
        {
            selectedVehicle.Dispose();
            SelectedVehicle.Dispose();
        }

        private void DeleteVehicle(object selectedItems)
        {
            var vehicleViewModels = SelectedItemsConverter<VehicleViewModel>.ConvertToArray(selectedItems);

            foreach (var vehicle in vehicleViewModels)
            {
                Vehicles.Remove(vehicle);
                SelectedVehicle = Vehicles.FirstOrDefault();
                Task.Run(() => vehicleService.Delete(vehicle.Model.Id)).Wait();
            }
        }
        private void UpdateVehicle()
        {
            Task.Run(() => vehicleService.Update(selectedVehicle.Model)).Wait();
            MessageBox.Show("Véhicule enregistré !", "Appliquer", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowOptionWindow()
        {
            var addOptionWindow = new AddOption(selectedVehicle, vehicleService, optionService);
            addOptionWindow.ShowDialog();
        }

        private void ShowCreateVehicleWindow()
        {
            var createVehicleWindow = new CreateVehicle(Vehicles, vehicleService, optionService, chassisService);
            createVehicleWindow.ShowDialog();
        }
    }
}
