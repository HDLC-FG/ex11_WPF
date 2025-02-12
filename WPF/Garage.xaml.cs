using System;
using System.Linq;
using System.Windows;
using WPF.ViewModels.Windows;

namespace WPF
{
    /// <summary>
    /// Logique d'interaction pour GarageWindow.xaml
    /// </summary>
    public partial class GarageWindow : Window
    {
        private readonly IGarageViewModel viewModel;

        public GarageWindow(IGarageViewModel viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel; 
            
            InitializeComponent();

            VehicleList.SelectedItem = viewModel.Vehicles.FirstOrDefault();
        }

        protected override void OnClosed(EventArgs e)
        {
            viewModel.Dispose();
            base.OnClosed(e);
        }
    }
}
