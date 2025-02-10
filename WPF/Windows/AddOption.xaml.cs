using System.Windows;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using WPF.Shared.UserControls;
using WPF.ViewModels;
using WPF.ViewModels.Entities;

namespace WPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour Option.xaml
    /// </summary>
    public partial class AddOption : Window
    {
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register(
                "ButtonCommand",
                typeof(ICommand),
                typeof(AddRoundButton),
                new PropertyMetadata(null));

        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }

        public AddOption(VehicleViewModel selectedVehicle, IVehicleService vehicleService, IOptionService optionService)
        {                        
            DataContext = new AddOptionViewModel(selectedVehicle, optionService, this);

            InitializeComponent();
        }
    }
}
