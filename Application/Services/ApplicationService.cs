using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Helper;
using Application.Structures;
using Domain.Entities.Customer;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly AmountConfiguration _amountConfiguration;
        decimal dailyLimit = 0;
        
        public ApplicationService(IUnitOfWork unitOfWork, IConfiguration configuration, IOptions<AmountConfiguration> amountConfiguration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _amountConfiguration = amountConfiguration.Value;
        }
        
        public async Task<Domain.Entities.Currencies.CurrencyRate> GetCurrencyRate(string code)
        {
            var currencyRate = await _unitOfWork.CurrencyRate
                .Include(i => i.Currency)
                .Where(x => x.Currency.Code.Equals(code))
                .OrderByDescending(o => o.TimeStamp)
                .FirstOrDefaultAsync();
            return currencyRate;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _unitOfWork.Customer.TableNoTracking.ToListAsync();
        } 
        
        public async Task<Domain.Entities.Customer.Customer> GetCustomer(string personalNumber)
        {
            return await _unitOfWork.Customer.TableNoTracking.FirstOrDefaultAsync(
                e => e.PersonalNumber == personalNumber);
        }
        
        public async Task<bool> CalculateDailyLimit(IEnumerable<TreeItem<Domain.Entities.Customer.Customer>> customers, int deep = 0)
        {
            if(dailyLimit > _amountConfiguration.DailyAmountLimit)
            {
                return true;
            }
            else
            {
                foreach (var c in customers)
                {
                    dailyLimit += await GetDailyTransactionAmount(c.Item.PersonalNumber, DateTime.Now);
                    CalculateDailyLimit(c.Children, deep + 1);
                }
            }
            return false;
        }
        
        public async Task<decimal> GetDailyTransactionAmount(string personalNumber, DateTime date)
        {
            var rangeDate = DateHelper.GetDateRange(date);
            
            var transactions = await _unitOfWork.Transaction.TableNoTracking
                .Where(t => t.PersonalNumber == personalNumber && t.TimeStamp > rangeDate.Item1 &&
                            t.TimeStamp < rangeDate.Item2).ToListAsync();

            decimal amount = 0;
            foreach (var transaction in transactions)
            {
                var currencyRate = await GetCurrencyRate(transaction.ToCurrency);
                amount += transaction.ToAmount * currencyRate.Buy;
            }

            return amount;
        }
    }
}