using FluentValidation;

namespace AutoAid.Domain.Dto.Place
{
    public class CreatePlaceDto
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class CreatePlaceDtoValidator : AbstractValidator<CreatePlaceDto>
    {
        public CreatePlaceDtoValidator()
        {
            RuleFor(x => x.Lat)
                .NotEmpty().WithMessage("Lat is required")
                .GreaterThan(100).WithMessage("Lat is invalid")
                .LessThan(50).WithMessage("Lat is invalid");
            RuleFor(x => x.Lng)
                .NotEmpty().WithMessage("Lng is required")
                .Must(x => x > 0);
        }
    }
}
