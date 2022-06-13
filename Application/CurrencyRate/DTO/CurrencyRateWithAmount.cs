namespace Application.CurrencyRate.DTO
{
    public class CurrencyRateWithAmount
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
    }
}