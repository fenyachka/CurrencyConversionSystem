using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Structures;
using Domain.Entities.Customer;

namespace Application.Services
{
    public interface IApplicationService
    {
        Task<Domain.Entities.Currencies.CurrencyRate> GetCurrencyRate(string code);
        Task<List<Domain.Entities.Customer.Customer>> GetCustomers();
        Task<Domain.Entities.Customer.Customer> GetCustomer(string personalNumber);
        Task<bool> CalculateDailyLimit(IEnumerable<TreeItem<Domain.Entities.Customer.Customer>> customers, int deep = 0);
        Task<decimal> GetDailyTransactionAmount(string personalNumber, DateTime dateTime);
        Task<int> AllTransactionCount(IEnumerable<TreeItem<Domain.Entities.Customer.Customer>> customers,
            DateTime? fromDate, DateTime? toDate, int deep = 0);
        Task<int> GetDailyTransactionCount(string personalNumber, DateTime? fromDate, DateTime? toDate);
    }
}