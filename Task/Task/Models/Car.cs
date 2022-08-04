using Task.Models.Base;

namespace Task.Models
{
    public class Car:BaseEntity
    {

        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool Display { get; set; }
        public int? EngineId { get; set; }
        public Engine Engine { get; set; }
    }
}
