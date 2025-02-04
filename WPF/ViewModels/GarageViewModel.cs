using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Interfaces.ViewModels;
using ApplicationCore.Models;
using WPF.Events;
using static ApplicationCore.Enums;

namespace WPF.ViewModels
{
    public class GarageViewModel : NotifyPropertyChanged, IGarageViewModel
    {
        private readonly IVehicleService vehicleService;
        private readonly IOptionService optionService;
        private Vehicle selectedVehicle;

        public GarageViewModel(IVehicleService vehicleService, IOptionService optionService)
        {
            this.vehicleService = vehicleService;
            this.optionService = optionService;

            EngineTypes = Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToList();

            var vehicles = Task.Run(() => vehicleService.GetAll()).Result;
            Vehicles = new ObservableCollection<Vehicle>(vehicles);
        }

        public IList<EngineType> EngineTypes { get; set; }
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
            }
        }
        public ICommand UpdateVehicleCommand => new Command(execute => UpdateVehicle());
        public ICommand ShowOptionsCommand => new Command(execute => ShowOptionWindow());

        private void UpdateVehicle()
        {
            Task.Run(() => vehicleService.Update(selectedVehicle)).Wait();
        }

        private void ShowOptionWindow()
        {
            var optionWindow = new Windows.Option(selectedVehicle, vehicleService, optionService);
            optionWindow.ShowDialog();
        }
    }
}
