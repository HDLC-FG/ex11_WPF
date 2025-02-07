using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using WPF.Events;
using static ApplicationCore.Enums;

namespace WPF.ViewModels
{
    public class CreateVehicleViewModel : NotifyPropertyChanged
    {
        private readonly IVehicleService vehicleService;
        private readonly IOptionService optionService;
        private Chassis selectedChassis;
        private Vehicle selectedVehicle;

        public ObservableCollection<Chassis> Chassis { get; set; }
        public IList<EngineType> EngineTypes { get; set; }
        public Chassis SelectedChassis
        {
            get { return selectedChassis; }
            set
            {
                selectedChassis = value;
                SelectedVehicle = new Vehicle
                {
                    Chassis = selectedChassis,
                    Engine = new Engine(),
                    Options = new ObservableCollection<Option>()
                };
                OnPropertyChanged();
            }
        }
        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
            }
        }

        public CreateVehicleViewModel(Vehicle vehicle, IVehicleService vehicleService, IOptionService optionService, IChassisService chassisService, Windows.CreateVehicle createVehicle)
        {
            this.vehicleService = vehicleService;
            this.optionService = optionService; 
            
            var chassis = Task.Run(() => chassisService.GetAll()).Result;
            Chassis = new ObservableCollection<Chassis>(chassis);

            EngineTypes = Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToList();
        }

        public ICommand CreateVehicleCommand => new Command(execute => CreateVehicle());
        public ICommand AddOptionsCommand => new Command(execute => ShowOptionWindow());

        private void CreateVehicle()
        {
            Task.Run(() => vehicleService.Add(SelectedVehicle)).Wait();
        }

        private void ShowOptionWindow()
        {
            var optionWindow = new Windows.Option(SelectedVehicle, vehicleService, optionService);
            optionWindow.ShowDialog();
        }
    }
}
