namespace Application.CurrencyRate.DTO
{
    public class ExchangeDto
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal AmountFrom { get; set; }
        public string PersonalNumber { get; set; }
    }
}