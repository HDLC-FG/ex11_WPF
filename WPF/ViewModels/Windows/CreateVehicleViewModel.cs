using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using WPF.Events;
using WPF.ViewModels.Entities;
using WPF.Windows;
using static ApplicationCore.Enums;

namespace WPF.ViewModels.Windows
{
    public class CreateVehicleViewModel : NotifyPropertyChanged, IDisposable
    {
        private readonly IList<VehicleViewModel> garageVehicles;
        private readonly IVehicleService vehicleService;
        private readonly IOptionService optionService;
        private readonly CreateVehicle createVehicleWindow;
        private ChassisViewModel selectedChassis;
        private VehicleViewModel selectedVehicle;

        public CreateVehicleViewModel(
            IList<VehicleViewModel> garageVehicles, 
            IVehicleService vehicleService, 
            IOptionService optionService, 
            IChassisService chassisService, 
            CreateVehicle createVehicleWindow)
        {
            this.garageVehicles = garageVehicles;
            this.vehicleService = vehicleService;
            this.optionService = optionService;
            this.createVehicleWindow = createVehicleWindow;

            var chassis = Task.Run(() => chassisService.GetAll()).Result;
            Chassis = new ObservableCollection<ChassisViewModel>(chassis.Select(c => new ChassisViewModel(c)));

            EngineTypes = Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToList();
        }

        public ObservableCollection<ChassisViewModel> Chassis { get; set; }
        public IList<EngineType> EngineTypes { get; set; }
        public ChassisViewModel SelectedChassis
        {
            get { return selectedChassis; }
            set
            {
                selectedChassis = value;
                SelectedVehicle = new VehicleViewModel(selectedChassis);
                OnPropertyChanged();
            }
        }
        public VehicleViewModel SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateVehicleCommand => new Command(execute => CreateVehicle());
        public ICommand AddOptionsCommand => new Command(execute => ShowOptionWindow());

        public void Dispose()
        {
            selectedVehicle.Dispose();
            SelectedVehicle.Dispose();
        }

        private void CreateVehicle()
        {
            Task.Run(() => vehicleService.Add(selectedVehicle.Model)).Wait();
            garageVehicles.Add(selectedVehicle);
            createVehicleWindow.Close();
        }

        private void ShowOptionWindow()
        {
            var optionWindow = new AddOption(SelectedVehicle, optionService);
            optionWindow.ShowDialog();
        }
    }
}
