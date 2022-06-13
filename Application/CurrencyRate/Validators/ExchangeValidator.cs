using Application.CurrencyRate.DTO;
using FluentValidation;

namespace Application.CurrencyRate.Validators
{
    public class ExchangeValidator : AbstractValidator<ExchangeDto>
    {
        public ExchangeValidator()
        {
            RuleFor(p => p.AmountFrom).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(p => p.FromCurrency).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(p => p.ToCurrency).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(p => p.PersonalNumber).MaximumLength(11);
        }
    }
}