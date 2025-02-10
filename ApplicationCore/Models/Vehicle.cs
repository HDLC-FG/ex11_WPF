using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public Chassis Chassis { get; set; }
        public virtual Engine Engine { get; set; }
        public virtual IList<Option> Options { get; set; } = new List<Option>();
    }
}
