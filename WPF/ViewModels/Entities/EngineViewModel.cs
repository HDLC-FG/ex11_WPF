using ApplicationCore.Models;
using static ApplicationCore.Enums;

namespace WPF.ViewModels.Entities
{
    public class EngineViewModel : NotifyPropertyChanged
    {
        private int price;

        public EngineViewModel()
        {
            Model = new Engine();
        }

        public EngineViewModel(Engine engine)
        {
            Model = engine;
            Id = engine.Id;
            Horsepower = engine.Horsepower;
            price = engine.Price;
            Type = engine.Type;
        }

        public Engine Model { get; private set; }
        public int Id { get; set; }
        public int Horsepower { get; set; }
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(VehicleViewModel.TotalPrice));
            }
        }
        public EngineType Type { get; set; }


        public override string ToString()
        {
            return $"{Type}, {Horsepower} chevaux, {Price}€";
        }
    }
}
