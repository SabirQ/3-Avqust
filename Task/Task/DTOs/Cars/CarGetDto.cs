﻿namespace Task.DTOs.Cars
{
    public class CarGetDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool Display { get; set; } = true;
    }
}
