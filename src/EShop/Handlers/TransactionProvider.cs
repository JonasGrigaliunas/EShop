using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using EShop.Data;

namespace EShop.Handlers
{
    public class TransactionProvider : ITransactionProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransaction GetTransaction()
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;
            options.Timeout = TimeSpan.FromSeconds(30);
            options.IsolationLevel = IsolationLevel.ReadCommitted;
            return new Transaction(new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled));
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public interface ITransaction : IDisposable
        {
            void Commit();
        }

        private class Transaction : ITransaction
        {
            private readonly TransactionScope _transactionScope;

            public Transaction(TransactionScope transactionScope)
            {
                _transactionScope = transactionScope;
            }

            public void Commit()
            {
                _transactionScope.Complete();
            }

            public void Dispose()
            {
                _transactionScope?.Dispose();
            }
        }
    }
}
