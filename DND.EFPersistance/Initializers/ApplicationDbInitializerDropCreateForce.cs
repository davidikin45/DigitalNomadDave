using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND.EFPersistance;
using DND.Core.Model;

using System.Data.Entity;
using DND.EFPersistance.Migrations;
using Solution.Base.Implementation.Persistance;

namespace DND.EFPersistance.Initializers
{
    //Will still pick up ApplicationDbConfiguration Migrations Config
    public class ApplicationDbInitializerDropCreateForce : BaseDropCreateForceDatabaseInitializer<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Database.CommandTimeout = 180;
            DBSeed.Seed(context);            
            base.Seed(context);
        }       
    }
  
}
