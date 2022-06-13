using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Currencies;
using Domain.Entities.Customer;
using Domain.Entities.Transaction;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveAsync(CancellationToken cancellationToken);
        IRepository<Currency> Currency { get; }
        IRepository<CurrencyRate> CurrencyRate { get; }
        IRepository<Customer> Customer { get; }
        IRepository<Transaction> Transaction { get; }
    }
}