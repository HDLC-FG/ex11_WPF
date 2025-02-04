using System.Linq;
using System.Windows;
using ApplicationCore.Interfaces.ViewModels;

namespace WPF
{
    /// <summary>
    /// Logique d'interaction pour GarageWindow.xaml
    /// </summary>
    public partial class GarageWindow : Window
    {
        public GarageWindow(IGarageViewModel viewModel)
        {
            DataContext = viewModel; 
            
            InitializeComponent();

            VehicleList.SelectedItem = viewModel.Vehicles.FirstOrDefault();
        }
    }
}
