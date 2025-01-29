using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public virtual Engine Engine { get; set; }
        public virtual IList<Option> Options { get; set; } = new List<Option>();
        public decimal TotalPrice
        {
            get
            {
                return Price + Engine.Price + Options.Sum(x => x.Price);
            }
        }
    }
}
