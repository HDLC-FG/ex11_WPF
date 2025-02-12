using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF.Converters;
using WPF.Events;
using WPF.ViewModels.Entities;
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
                typeof(VehicleViewModel),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public VehicleViewModel DataContextVehicle
        {
            get { return (VehicleViewModel)GetValue(DataContextVehicleProperty); }
            set { SetValue(DataContextVehicleProperty, value); }
        }

        public static readonly DependencyProperty EngineTypesContextProperty =
            DependencyProperty.Register(
                "EngineTypes",
                typeof(List<EngineType>),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public IList<EngineType> EngineTypes
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

        public static readonly DependencyProperty ApplyLabelProperty =
            DependencyProperty.Register(
                "ApplyLabel",
                typeof(string),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata("Appliquer"));

        public string ApplyLabel
        {
            get { return (string)GetValue(ApplyLabelProperty); }
            set { SetValue(ApplyLabelProperty, value); }
        }

        public static readonly DependencyProperty ApplyBackgroundProperty =
           DependencyProperty.Register(
               "ApplyBackground",
               typeof(SolidColorBrush),
               typeof(AddOrUpdateVehicle),
               new PropertyMetadata(Application.Current.Resources["IsSelectedColorBrush"]));

        public SolidColorBrush ApplyBackground
        {
            get { return (SolidColorBrush)GetValue(ApplyBackgroundProperty); }
            set { SetValue(ApplyBackgroundProperty, value); }
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

        public ICommand DeleteOptionCommand => new Command(execute => DeleteOption(execute), canExecute => Command.IsNotNullOrEmpty(canExecute));

        private void DeleteOption(object selectedItems)
        {
            var options = SelectedItemsConverter<OptionViewModel>.ConvertToArray(selectedItems);

            foreach (var option in options)
            {
                DataContextVehicle.DeleteOption(option);
            }
        }
    }
}
