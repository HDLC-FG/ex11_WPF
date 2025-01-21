using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPF.MVVM.Models;

namespace WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            VehicleList.DataContext = new ObservableCollection<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Name = "208",
                    Brand = "Peugeot",
                    Engine = new Engine
                    {
                        Id = 1,
                        Horsepower = 100,
                        Price = 8000,
                        Type = MVVM.Enums.TypeEngine.Diesel
                    },
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Id = 1,
                            Name = "Radar de recul",
                            Price = 1000
                        },
                        new Option
                        {
                            Id = 2,
                            Name = "Boite automatique",
                            Price = 4000
                        }
                    },
                    Price = 12000
                }
            };
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AddVehicle_Click");
        }
    }
}
