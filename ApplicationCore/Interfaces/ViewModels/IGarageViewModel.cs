using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.ViewModels
{
    public interface IGarageViewModel
    {
        IList<Enums.EngineType> EngineTypes { get; set; }
        ObservableCollection<Vehicle> Vehicles { get; set; }
        Vehicle SelectedVehicle { get; set; }
        ICommand ShowOptionsCommand { get; }
        ICommand UpdateVehicleCommand { get; }
        ICommand AddOptionsCommand { get; }
        ICommand CreateVehicleCommand { get; }
    }
}