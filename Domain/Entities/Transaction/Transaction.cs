using System;

namespace Domain.Entities.Transaction
{
    public class Transaction : BaseEntity<int>
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}