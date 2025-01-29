using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
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
            Task.Run(() => vehicleService.Add(new Vehicle
            {
                //Id = 1,
                Name = "Car 1",
                Brand = "Brand A",
                Price = 20000,
                Engine = new Engine
                {
                    //Id = 1,
                    Horsepower = 100,
                    Price = 15000,
                    Type = Enums.TypeEngine.Petrol
                },
                Options = new List<Option>
                    {
                        new Option
                        {
                            //Id = 1,
                            Name = "Option 1",
                            Price = 1000
                        },
                        new Option
                        {
                            //Id = 2,
                            Name = "Option 2",
                            Price = 1200
                        },
                        new Option
                        {
                            //Id = 3,
                            Name = "Option 3",
                            Price = 800
                        }
                    }
            })).Wait();
            Task.Run(() => vehicleService.Add(new Vehicle
            {
                Id = 2,
                Name = "Car 2",
                Brand = "Brand B",
                Price = 25000,
                Engine = new Engine
                {
                    Id = 2,
                    Horsepower = 200,
                    Price = 25000,
                    Type = Enums.TypeEngine.Electric
                },
                Options = new List<Option>
                {
                    new Option
                    {
                        Id = 2,
                        Name = "Option 2",
                        Price = 1200
                    }
                }
            })).Wait();
            Task.Run(() => vehicleService.Add(new Vehicle
            {
                Id = 3,
                Name = "Car 3",
                Brand = "Brand C",
                Price = 30000,
                Engine = new Engine
                {
                    Id = 3,
                    Horsepower = 150,
                    Price = 30000,
                    Type = Enums.TypeEngine.Hybrid
                },
                Options = new List<Option>
                    {
                        new Option
                        {
                            Id = 3,
                            Name = "Option 3",
                            Price = 1500
                        }
                    }
            })).Wait();

            EngineTypes = Enum.GetValues(typeof(TypeEngine)).Cast<TypeEngine>().ToList();

            var vehicleList = Task.Run(() => vehicleService.GetAll()).Result;
            Vehicles = new ObservableCollection<Vehicle>(vehicleList);
            this.vehicleService = vehicleService;
        }

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
        public List<TypeEngine> EngineTypes { get; set; }
        public Command UpdateVehicleCommand => new Command(execute => UpdateVehicle());

        private void UpdateVehicle()
        {
            Task.Run(() => vehicleService.Update(selectedVehicle)).Wait();
        }
    }
}
