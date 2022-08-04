using FluentValidation;

namespace Task.DTOs.Cars
{
    public class CarPostDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool Display { get; set; } 
    }
    public class CarPostDtoValidation:AbstractValidator<CarPostDto>
    {
        public CarPostDtoValidation()
        {
            RuleFor(c => c.Brand).NotNull().WithMessage("enter value").MaximumLength(20).WithMessage("maximum length must be 20");
            RuleFor(c => c.Model).NotNull().WithMessage("enter value").MaximumLength(30).WithMessage("maximum length must be 30");
            RuleFor(c => c.Brand).NotNull().WithMessage("enter value").MaximumLength(15).WithMessage("maximum length must be 15");
            RuleFor(c => c.Price).NotNull().WithMessage("enter value")
                .GreaterThanOrEqualTo(3000.00m).WithMessage("we do not have car cheaper than 3000.00")
                .LessThanOrEqualTo(9999999.99m).WithMessage("max value must be 9999999.99");

        }
    }
}
