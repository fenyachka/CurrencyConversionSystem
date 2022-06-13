using Application.CurrencyRate.DTO;
using FluentValidation;

namespace Application.CurrencyRate.Validators
{
    public class ExchangeRateValidator : AbstractValidator<ExchangeRateDto>
    {
        public ExchangeRateValidator()
        {
            RuleFor(p => p.AmountFrom).NotNull().NotEmpty();
            RuleFor(p => p.FromCurrency).NotNull().NotEmpty().Length(3);
            RuleFor(p => p.ToCurrency).NotNull().NotEmpty().Length(3);
        }
    }
}