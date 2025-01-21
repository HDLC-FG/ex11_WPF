using System.Collections.ObjectModel;
using System.Collections.Generic;
using WPF.MVVM.Models;

namespace WPF.MVVM.ViewModels
{
    public class VehicleList
    {
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        public VehicleList()
        {
            Vehicles = new ObservableCollection<Vehicle>
            {
                new Vehicle
                { 
                    Id = 1, 
                    Name = "Car 1", 
                    Brand = "Brand A", 
                    Price = 20000,
                    Engine = new Engine
                    {
                        Id = 1,
                        Horsepower = 100,
                        Price = 15000,
                        Type = Enums.TypeEngine.Petrol
                    },
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Id = 1,
                            Name = "Option 1",
                            Price = 1000
                        }
                    }
                },
                new Vehicle 
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
                },
                new Vehicle 
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
                }
            };
        }
    }
}
