using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND.Core.Interfaces.Persistance;
using Solution.Base.Interfaces.Persistance;
using DND.Core.Model;

namespace DND.EFPersistance
{
    public class DbContextFactory : IDbContextFactory
    {

        public IBaseDbContext CreateDefault()
        {
            return new ApplicationDbContext();
        }

        public TIDbContext Create<TIDbContext>() where TIDbContext : IBaseDbContext
        {
            if (typeof(TIDbContext) == typeof(IApplicationDbContext) || typeof(TIDbContext) == typeof(IBaseDbContext))
            {
                return (TIDbContext)CreateDefault();
            }
            else
            {
                throw new ApplicationException("DbContext Type Unknown");
            }
        }
    }
}
