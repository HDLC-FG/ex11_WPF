namespace WPF.MVVM.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Price.ToString("C")})";
        }
    }
}
