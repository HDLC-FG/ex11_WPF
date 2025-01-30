using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using WPF.Events;
using static ApplicationCore.Enums;

namespace WPF.ViewModels
{
    internal class GarageViewModel : NotifyPropertyChanged
    {
        private readonly IVehicleService vehicleService;
        private Vehicle selectedVehicle;

        public GarageViewModel(IVehicleService vehicleService)
        {
            EngineTypes = Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToList();

            var vehicleList = Task.Run(() => vehicleService.GetAll()).Result;
            Vehicles = new ObservableCollection<Vehicle>(vehicleList);
            this.vehicleService = vehicleService;
        }

        public List<EngineType> EngineTypes { get; set; }
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
        public Command UpdateVehicleCommand => new Command(execute => UpdateVehicle());

        private void UpdateVehicle()
        {
            Task.Run(() => vehicleService.Update(selectedVehicle)).Wait();
        }
    }
}
