using ApplicationCore.Models;

namespace WPF.ViewModels.Entities
{
    public class OptionViewModel
    {
        public OptionViewModel(Option option)
        {
            Model = option;
            Id = option.Id;
            Name = option.Name;
            Price = option.Price;
        }

        public Option Model { get; private set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        public override string ToString()
        {
            return $"{Name} ({Price.ToString("C")})";
        }
    }
}
