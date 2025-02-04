using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using WPF.Events;

namespace WPF.ViewModels
{
    public class OptionViewModel : NotifyPropertyChanged
    {
        private readonly IVehicleService vehicleService;
        private readonly Windows.Option window;
        private Vehicle selectedVehicle;

        public OptionViewModel(Vehicle selectedVehicle, IVehicleService vehicleService, IOptionService optionService, Windows.Option window)
        {
            this.selectedVehicle = selectedVehicle;
            this.vehicleService = vehicleService;
            this.window = window;
            var options = Task.Run(() => optionService.GetAvailables(selectedVehicle.Options)).Result;
            Options = new ObservableCollection<Option>(options);
        }

        public ObservableCollection<Option> Options { get; set; }
        public ObservableCollection<Option> SelectedOptions { get; set; }
        public ICommand AddCommand => new Command(AddOptions, canExecute => true);

        private void AddOptions(object selectItems)
        {
            var collection = (IList)selectItems;
            Option[] options = new Option[collection.Count];
            collection.CopyTo(options, 0);

            foreach (Option option in options)
            {
                selectedVehicle.Options.Add(option);
            }

            Task.Run(() => vehicleService.Update(selectedVehicle)).Wait();

            window.Close();
        }
    }
}
