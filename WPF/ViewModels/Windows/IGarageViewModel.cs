﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ApplicationCore;
using WPF.ViewModels.Entities;

namespace WPF.ViewModels.Windows
{
    public interface IGarageViewModel : IDisposable
    {
        IList<Enums.EngineType> EngineTypes { get; set; }
        ObservableCollection<VehicleViewModel> Vehicles { get; set; }
        VehicleViewModel SelectedVehicle { get; set; }
        ICommand UpdateVehicleCommand { get; }
        ICommand AddOptionsCommand { get; }
        ICommand CreateVehicleCommand { get; }
        ICommand DeleteVehicleCommand { get; }
    }
}