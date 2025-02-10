using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using WPF.Converters;
using WPF.Events;
using WPF.ViewModels.Entities;
using WPF.Windows;

namespace WPF.ViewModels.Windows
{
    public class AddOptionViewModel : NotifyPropertyChanged
    {
        private readonly AddOption window;
        private VehicleViewModel selectedVehicle;

        public ObservableCollection<OptionViewModel> Options { get; set; }
        public ObservableCollection<OptionViewModel> SelectedOptions { get; set; }
        public ICommand AddCommand => new Command(execute => AddOptions(execute), canExecute => true);

        public AddOptionViewModel(VehicleViewModel selectedVehicle, IOptionService optionService, AddOption window)
        {
            this.selectedVehicle = selectedVehicle;
            this.window = window;
            var options = Task.Run(() => optionService.GetAvailables(selectedVehicle.Model.Options)).Result;
            Options = new ObservableCollection<OptionViewModel>(options.Select(option => new OptionViewModel(option)));
        }


        private void AddOptions(object selectedItems)
        {
            var options = SelectedItemsConverter<OptionViewModel>.ConvertToArray(selectedItems);

            foreach (var option in options)
            {
                selectedVehicle.AddOption(option);
            }

            window.Close();
        }
    }
}
