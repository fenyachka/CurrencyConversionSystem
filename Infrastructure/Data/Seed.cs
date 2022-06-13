using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Currencies;
using Domain.Entities.Customer;
using Domain.Entities.Transaction;

namespace Infrastructure.Config
{
    public class Seed
    {
         public static async Task SeedData(DataContext context)
        {
            if (context.Currencies.Any()) return;
            var currencies = new List<Currency>()
            {
                new Currency
                {
                    Code = "GEL",
                    NameGeo = "ლარი",
                    NameLat = "Lari"
                },
                new Currency
                {
                    Code = "USD",
                    NameGeo = "დოლარი",
                    NameLat = "Dollar"
                },
                new Currency
                {
                    Code = "EUR",
                    NameGeo = "ევრო",
                    NameLat = "Euro"
                }
            };
            await context.Currencies.AddRangeAsync(currencies);
            
            if (context.CurrencyRates.Any()) return;
            var currencyRates = new List<CurrencyRate>()
            {
                new CurrencyRate
                {
                    Currency =  new Currency
                    {
                        Code = "USD",
                        NameGeo = "დოლარი",
                        NameLat = "Dollar"
                    },
                    Buy = (decimal) 3.12,
                    Sell = (decimal) 3.10
                },
                new CurrencyRate
                {
                    Currency = new Currency
                    {
                        Code = "EUR",
                        NameGeo = "ევრო",
                        NameLat = "Euro"
                    },
                    Buy = (decimal) 3.42,
                    Sell = (decimal) 3.22
                }
            };
            
            await context.CurrencyRates.AddRangeAsync(currencyRates);
            
            if (context.Customers.Any()) return;
            var customers = new List<Customer>()
            {
                new Customer
                {
                    Id = new Guid(),
                    FirstName = "root",
                    LastName = "root",
                    PersonalNumber = "00000000011",
                    RecommenderPersonalNumber = "00000000111"
                },
                new Customer
                {
                    Id = new Guid(),
                    FirstName = "James",
                    LastName = "Bond",
                    PersonalNumber = "00000000000",
                    RecommenderPersonalNumber = "00000000011"
                },
                new Customer
                {
                    Id = new Guid(),
                    FirstName = "Ana",
                    LastName = "Clarcke",
                    PersonalNumber = "00000000001",
                    RecommenderPersonalNumber = "00000000000"
                }
            };
            
            await context.Customers.AddRangeAsync(customers);
            
            
            if (context.Transactions.Any()) return;
            var transactions = new List<Transaction>()
            {
                new Transaction
                {
                    FromAmount = 2,
                    ToAmount = 6,
                    FromCurrency = "USD",
                    ToCurrency = "GEL",
                    PersonalNumber = "00000000000",
                    TimeStamp = Convert.ToDateTime("2022-06-12 13:30:00.0000000")
                },
                new Transaction
                {
                    FromAmount = 3,
                    ToAmount = 9,
                    FromCurrency = "USD",
                    ToCurrency = "GEL",
                    PersonalNumber = "00000000001",
                    TimeStamp = Convert.ToDateTime("2022-06-12 13:30:00.0000000")
                }
            };
            
            await context.Transactions.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }
    }
}