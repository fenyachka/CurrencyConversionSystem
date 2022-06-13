using FluentValidation;

namespace Application.CurrencyRate.Validators
{
    public class CurrencyRateDtoValidator : AbstractValidator<GetCurrencyRate.Query>
    {
        public CurrencyRateDtoValidator()
        {
            RuleFor(x => x.CurrencyCode).NotEmpty().MaximumLength(3);
        }
    }
}