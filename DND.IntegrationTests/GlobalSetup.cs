using DND.EFPersistance.Migrations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using DND.Core.Models;
using DND.EFPersistance;
using Solution.Base.Infrastructure;
using DND.EFPersistance.Initializers;
using Solution.Base.Implementation.Models;

namespace DND.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void Setup()
        {
            SetConfiguration();
            MigrateDbToLatestVersion();
            Seed();
        }

        public void SetConfiguration()
        {
            SqlProviderServices.SqlServerTypesAssemblyName = "Microsoft.SqlServer.Types, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
        }

        public void MigrateDbToLatestVersion()
        {
            DbContextInitializer<ApplicationDbContext>.SetInitializer(new ApplicationDbInitializerDropCreateForce(), true, true);
           
            //var configuration = new ApplicationDbConfiguration();
            //var migrator = new DbMigrator(configuration);
            //migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new User { UserName = "user1", Name="user1", Email="-", PasswordHash="-" });
            context.Users.Add(new User { UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });
            context.SaveChanges();
        }
    }
}
