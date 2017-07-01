using DND.EFPersistance;
using DND.EFPersistance.Initializers;
using Solution.Base.Tasks;
using Solution.Base.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.EFPersistance
{
    public class ApplicationDbInitializer : IRunAtInit
    {
        public void Execute()
        {
            //DbContextInitializer<ApplicationDbContext>.SetInitializer(new ApplicationDbInitializerDropCreate(), true, true);
            DbContextInitializer<ApplicationDbContext>.SetInitializer(new ApplicationDbInitializerMigrate(), true, true);
        }
    }
}
