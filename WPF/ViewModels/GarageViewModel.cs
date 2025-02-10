﻿using System;
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

        private void UpdateVehicle()
        {
            Task.Run(() => vehicleService.Update(selectedVehicle.Model)).Wait();
        }

        private void ShowOptionWindow()
        {
            var addOptionWindow = new Windows.AddOption(selectedVehicle, vehicleService, optionService);
            addOptionWindow.ShowDialog();
        }

        private void ShowCreateVehicleWindow()
        {
            var createVehicleWindow = new Windows.CreateVehicle(vehicleService, optionService, chassisService);
            createVehicleWindow.ShowDialog();
        }
    }
}
