using ApplicationCore.Models;

namespace WPF.ViewModels.Entities
{
    public class OptionViewModel
    {
        public OptionViewModel(Option option)
        {
            Model = option;
        }

        public Option Model { get; private set; }
        public int Id
        {
            get { return Model.Id; }
            set { Model.Id = value; }
        }
        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; }
        }
        public decimal Price
        {
            get { return Model.Price; }
            set { Model.Price = value; }
        }


        public override string ToString()
        {
            return $"{Name} ({Price.ToString("C")})";
        }
    }
}
