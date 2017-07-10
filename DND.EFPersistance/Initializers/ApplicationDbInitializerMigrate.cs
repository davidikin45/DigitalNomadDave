using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND.EFPersistance;
using DND.Core.Models;

using System.Data.Entity;
using DND.EFPersistance.Migrations;

namespace DND.EFPersistance.Initializers
{

    //https://blog.oneunicorn.com/2013/05/28/database-initializer-and-migrations-seed-methods/
    public class ApplicationDbInitializerMigrate : MigrateDatabaseToLatestVersion<ApplicationDbContext, ApplicationDbConfiguration>
    {
       
    }
   
}
