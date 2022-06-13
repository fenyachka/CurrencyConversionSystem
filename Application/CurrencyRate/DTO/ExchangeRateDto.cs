namespace Application.CurrencyRate.DTO
{
    public class ExchangeRateDto
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal AmountFrom { get; set; }
    }
}