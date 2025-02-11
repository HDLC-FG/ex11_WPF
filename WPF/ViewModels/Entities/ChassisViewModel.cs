using ApplicationCore.Models;

namespace WPF.ViewModels.Entities
{
    public class ChassisViewModel
    {
        public ChassisViewModel(Chassis model)
        {
            Model = model;
        }

        public Chassis Model { get; private set; }
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
        public string Brand
        {
            get { return Model.Brand; }
            set { Model.Brand = value; }
        }
        public decimal Price
        {
            get { return Model.Price; }
            set { Model.Price = value; }
        }
    }
}
