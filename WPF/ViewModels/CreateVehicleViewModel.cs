using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using WPF.Events;
using WPF.ViewModels.Entities;
using static ApplicationCore.Enums;

namespace WPF.ViewModels
{
    public class CreateVehicleViewModel : NotifyPropertyChanged
    {
        private readonly IVehicleService vehicleService;
        private readonly IOptionService optionService;
        private readonly Windows.CreateVehicle createVehicleWindow;
        private ChassisViewModel selectedChassis;
        private VehicleViewModel selectedVehicle;

        public CreateVehicleViewModel(IVehicleService vehicleService, IOptionService optionService, IChassisService chassisService, Windows.CreateVehicle createVehicleWindow)
        {
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

        private void CreateVehicle()
        {
            Task.Run(() => vehicleService.Add(SelectedVehicle.Model)).Wait();
            createVehicleWindow.Close();
        }

        private void ShowOptionWindow()
        {
            var optionWindow = new Windows.AddOption(SelectedVehicle, vehicleService, optionService);
            optionWindow.ShowDialog();
        }
    }
}
