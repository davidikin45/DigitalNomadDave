using Solution.Base.Interfaces.Persistance;
using Solution.Base.Tasks;
using System.Data.Entity;
using System.Transactions;
using System.Web;
using Solution.Base.Extensions;

namespace Solution.Base.Infrastructure
{
	public class TransactionPerRequest :
		IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        private readonly HttpContextBase _httpContext;
        private readonly IDbContextFactory _factory;

        public TransactionPerRequest(IDbContextFactory factory, HttpContextBase httpContext)
		{
			_httpContext = httpContext;
            _factory = factory;
        }

		void IRunOnEachRequest.Execute()
		{
            //_httpContext.Items["_Transaction"] = new TransactionScope();
            //_httpContext.Items["_Transaction"] = T_httpContext_dbContext.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        void IRunOnError.Execute()
        {
            //_httpContext.Items["_Error"] = true;
        }

        void IRunAfterEachRequest.Execute()
        {
            //var transaction = (TransactionScope)_httpContext.Items["_Transaction"];

            //if (_httpContext.Items["_Error"] != null)
            //{
            //    transaction.Dispose();
            //}
            //else
            //{
            //    transaction.Complete();
            //}
        }
    }
}