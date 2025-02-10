using ApplicationCore.Models;

namespace WPF.ViewModels.Entities
{
    public class ChassisViewModel
    {
        public ChassisViewModel(Chassis model)
        {
            Model = model;
            Id = model.Id;
            Name = model.Name;
            Brand = model.Brand;
            Price = model.Price;
        }

        public Chassis Model { get; private set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
    }
}
