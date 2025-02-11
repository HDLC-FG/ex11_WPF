using ApplicationCore.Models;
using static ApplicationCore.Enums;

namespace WPF.ViewModels.Entities
{
    public class EngineViewModel : NotifyPropertyChanged
    {
        public EngineViewModel()
        {
            Model = new Engine();
        }

        public EngineViewModel(Engine engine)
        {
            Model = engine;
        }

        public Engine Model { get; private set; }
        public int Id
        {
            get { return Model.Id; }
            set { Model.Id = value; }
        }
        public int Horsepower
        {
            get { return Model.Horsepower; }
            set 
            {
                Model.Horsepower = value;
                OnPropertyChanged(nameof(Horsepower));
            }
        }
        public int Price
        {
            get { return Model.Price; }
            set
            {
                Model.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public EngineType Type
        {
            get { return Model.Type; }
            set 
            {
                Model.Type = value;
                OnPropertyChanged(nameof(Type));
            }
        }


        public override string ToString()
        {
            return $"{Type}, {Horsepower} chevaux, {Price}€";
        }
    }
}
