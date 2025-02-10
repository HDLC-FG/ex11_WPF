using static ApplicationCore.Enums;

namespace ApplicationCore.Models
{
    public class Engine
    {
        public int Id { get; set; }
        public int Horsepower { get; set; }
        public int Price { get; set; }
        public EngineType Type { get; set; }

        public override string ToString()
        {
            return $"{Type}, {Horsepower} chevaux, {Price}€";
        }
    }
}
