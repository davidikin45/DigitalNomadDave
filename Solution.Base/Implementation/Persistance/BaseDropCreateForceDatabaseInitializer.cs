﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Implementation.Persistance
{
    public class BaseDropCreateForceDatabaseInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext, new()
    {
        public BaseDropCreateForceDatabaseInitializer()
        {
            
        }

        protected virtual void Seed(TContext context)
        {

        }

        public void InitializeDatabase(TContext context)
        {
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "USE master DROP DATABASE [" + context.Database.Connection.Database + "]");
            }

            context.Database.Create();

            Seed(context);
        }
    }
}
