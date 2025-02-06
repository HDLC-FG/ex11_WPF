using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ApplicationCore.Models;
using WPF.Converters;
using WPF.Events;
using static ApplicationCore.Enums;

namespace WPF.Shared.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AddOrUpdateVehicle.xaml
    /// </summary>
    public partial class AddOrUpdateVehicle : UserControl
    {
        public AddOrUpdateVehicle()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DataContextVehicleProperty =
            DependencyProperty.Register(
                "DataContextVehicle",
                typeof(Vehicle),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public Vehicle DataContextVehicle
        {
            get { return (Vehicle)GetValue(DataContextVehicleProperty); }
            set { SetValue(DataContextVehicleProperty, value); }
        }

        public static readonly DependencyProperty EngineTypesContextProperty =
            DependencyProperty.Register(
                "EngineTypes",
                typeof(List<EngineType>),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public List<EngineType> EngineTypes
        {
            get { return (List<EngineType>)GetValue(EngineTypesContextProperty); }
            set { SetValue(EngineTypesContextProperty, value); }
        }

        public static readonly DependencyProperty ApplyCommandProperty =
            DependencyProperty.Register(
                "ApplyCommand",
                typeof(ICommand),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public ICommand ApplyCommand
        {
            get { return (ICommand)GetValue(ApplyCommandProperty); }
            set { SetValue(ApplyCommandProperty, value); }
        }

        public static readonly DependencyProperty AddOptionsCommandProperty =
            DependencyProperty.Register(
                "AddOptionsCommand",
                typeof(ICommand),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public ICommand AddOptionsCommand
        {
            get { return (ICommand)GetValue(AddOptionsCommandProperty); }
            set { SetValue(AddOptionsCommandProperty, value); }
        }

        public ICommand DeleteOptionCommand => new Command(execute => DeleteOption(execute), canExecute => true);

        private void DeleteOption(object selectedItems)
        {
            var options = SelectedItemsConverter<Option>.ConvertToArray(selectedItems);
            var vehicle = DataContextVehicle;

            foreach (var option in options)
            {
                vehicle.Options.Remove(option);
            }
        }
    }
}
