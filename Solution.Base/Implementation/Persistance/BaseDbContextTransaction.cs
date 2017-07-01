using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Solution.Base.Interfaces.Persistance;

namespace Solution.Base.Implementation.Persistance
{
    class BaseDbContextTransaction : IBaseDbContextTransaction
    {
        readonly DbContextTransaction _transaction;
        bool _commited;

        public BaseDbContextTransaction(DbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            this._transaction = transaction;
        }

        public void Commit()
        {
            this._transaction.Commit();
            this._commited = true;
        }

        public void Rollback()
        {
            this._transaction.Rollback();
        }

        public void Dispose()
        {
            if (!this._commited)
            {
                try
                {
                    this._transaction.Rollback();
                }
                catch (Exception) { }
            }

            this._transaction.Dispose();
        }

    }
}
