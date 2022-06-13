using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Currencies;
using Domain.Entities.Customer;
using Domain.Entities.Transaction;
using Domain.Interfaces;

namespace Infrastructure.Config
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private Hashtable _repositories;
        public IRepository<Currency> Currency { get; }
        public IRepository<CurrencyRate> CurrencyRate { get; }
        public IRepository<Customer> Customer { get; }
        public IRepository<Transaction> Transaction { get; }

        public UnitOfWork(DataContext context, 
            IRepository<Currency> currency, 
            IRepository<CurrencyRate> currencyRate,
            IRepository<Customer> customer,
            IRepository<Transaction> transaction
        )
        {
            _context = context;
            Currency = currency;
            CurrencyRate = currencyRate;
            Customer = customer;
            Transaction = transaction;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}