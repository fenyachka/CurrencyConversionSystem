using Application.CurrencyRate.DTO;
using FluentValidation;

namespace Application.CurrencyRate.Validators
{
    public class CreateCurrencyRateDtoValidator : AbstractValidator<CreateCurrencyRateDto>
    {
        public CreateCurrencyRateDtoValidator()
        {
            RuleFor(p => p.CurrencyId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(p => p.Buy).NotNull().NotEmpty();
            RuleFor(p => p.Sell).NotNull().NotEmpty();
        }
    }
}