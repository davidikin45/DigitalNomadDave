using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Infrastructure
{
    public static class DbContextInitializer<TContext>
    where TContext : DbContext
    {
        static object _InitializeLock = new object();
        static bool _InitializeLoaded = false;

        /// <summary>
        /// Method to allow running a DatabaseInitializer exactly once
        /// </summary>   
        /// <param name="initializer">A Database Initializer to run</param>
        public static void SetInitializer(IDatabaseInitializer<TContext> initializer = null, Boolean initialize = false, Boolean forceInitialize = false)

        {
            if (_InitializeLoaded)
                return;

            // watch race condition
            lock (_InitializeLock)
            {
                // are we sure?
                if (_InitializeLoaded)
                    return;

                _InitializeLoaded = true;

                // force Initializer to load only once
                System.Data.Entity.Database.SetInitializer<TContext>(initializer);
                if (initialize)
                {
                    using (var context = Activator.CreateInstance<TContext>())
                    {
                        context.Database.Initialize(forceInitialize);
                    }
                }
            }
        }
    }
}
