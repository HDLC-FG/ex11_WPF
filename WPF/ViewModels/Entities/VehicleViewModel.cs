using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ApplicationCore.Models;

namespace WPF.ViewModels.Entities
{
    public class VehicleViewModel : INotifyPropertyChanged
    {
        private EngineViewModel engine;
        private ObservableCollection<OptionViewModel> options;

        public VehicleViewModel(ChassisViewModel selectedChassis)
        {
            Model = new Vehicle
            {
                Chassis = selectedChassis.Model
            };
            Chassis = selectedChassis;
            Engine = new EngineViewModel();
            Options = new ObservableCollection<OptionViewModel>();
        }

        public VehicleViewModel(Vehicle model)
        {
            Model = model;
            Chassis = new ChassisViewModel(model.Chassis);
            Engine = new EngineViewModel(model.Engine);
            Options = new ObservableCollection<OptionViewModel>(model.Options.Select(option => new OptionViewModel(option)));
        }

        public Vehicle Model { get; private set; }
        public int Id { get; set; }
        public ChassisViewModel Chassis { get; set; }
        public EngineViewModel Engine
        {
            get { return engine; }
            set
            {
                engine = value;
                if (engine != null) engine.PropertyChanged += Child_PropertyChanged;
            }
        }
        public ObservableCollection<OptionViewModel> Options
        {
            get { return options; }
            set
            {
                options = value;
                if (options != null) options.CollectionChanged += Options_CollectionChanged;
            }
        }
        public decimal TotalPrice
        {
            get
            {
                return Chassis.Price + Engine.Price + Options.Sum(x => x.Price);
            }
        }

        public void AddOption(OptionViewModel option)
        {
            Model.Options.Add(option.Model);
            Options.Add(option);
        }

        public void DeleteOption(OptionViewModel option)
        {
            Model.Options.Remove(option.Model);
            Options.Remove(option);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        private void Options_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPrice)));
        }
    }
}
