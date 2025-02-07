using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ApplicationCore.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public Chassis Chassis { get; set; }
        public virtual Engine Engine { get; set; }
        public virtual IList<Option> Options { get; set; } = new ObservableCollection<Option>();
        public decimal TotalPrice
        {
            get
            {
                return Chassis.Price + Engine.Price + Options.Sum(x => x.Price);
            }
        }
    }
}
