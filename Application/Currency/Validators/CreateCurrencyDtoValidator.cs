using Application.Currency.DTO;
using FluentValidation;

namespace Application.Currency.Validators
{
    public class CreateCurrencyDtoValidator : AbstractValidator<CreateCurrencyDto>
    {
        public CreateCurrencyDtoValidator()
        {
            RuleFor(p => p.Code).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(p => p.NameGeo).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(p => p.NameLat).NotNull().NotEmpty().MaximumLength(250);
        }
    }
}