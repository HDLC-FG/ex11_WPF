using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using WPF.Converters;
using WPF.Events;

namespace WPF.ViewModels
{
    public class OptionViewModel : NotifyPropertyChanged
    {
        private readonly Windows.Option window;
        private Vehicle selectedVehicle;

        public OptionViewModel(Vehicle selectedVehicle, IOptionService optionService, Windows.Option window)
        {
            this.selectedVehicle = selectedVehicle;
            this.window = window;
            var options = Task.Run(() => optionService.GetAvailables(selectedVehicle.Options)).Result;
            Options = new ObservableCollection<Option>(options);
        }

        public ObservableCollection<Option> Options { get; set; }
        public ObservableCollection<Option> SelectedOptions { get; set; }
        public ICommand AddCommand => new Command(execute => AddOptions(execute), canExecute => true);

        private void AddOptions(object selectedItems)
        {
            var options = SelectedItemsConverter<Option>.ConvertToArray(selectedItems);

            foreach (Option option in options)
            {
                selectedVehicle.Options.Add(option);
            }

            window.Close();
        }
    }
}
