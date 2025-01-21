using System.Collections.Generic;
using System.Linq;

namespace WPF.MVVM.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public Engine Engine { get; set; }
        public IList<Option> Options { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return Price + Engine.Price + Options.Sum(x => x.Price);
            }
        }
    }
}
