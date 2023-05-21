using FluentValidation;

namespace Service.Dtos.BrandDtos
{
    public class BrandDto
    {
        public string Name { get; set; }
    }

    public class BrandDtoValidator : AbstractValidator<BrandDto>
    {
        public BrandDtoValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Daxil etmelisen").MaximumLength(30);
        }
    }
}
