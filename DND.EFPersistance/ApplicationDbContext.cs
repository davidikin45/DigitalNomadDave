﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using DND.Core.Interfaces;
using DND.Core.Models;
using DND.Core.Interfaces.Persistance;
using DND.EFPersistance.Migrations;
using Solution.Base.Implementation.Persistance;
using DND.EFPersistance.Initializers;
using System.Data.Entity.SqlServer;

using Solution.Base.Implementation.Models;
using Solution.Base.Interfaces.Persistance;

namespace DND.EFPersistance
{
    public class ApplicationDbContext : BaseIdentityDbContext<User>, IApplicationDbContext
    {

        public IDbSet<BlogPost> BlogPosts { get; set; }
        public IDbSet<BlogPostTag> BlogPostTags { get; set; }
        public IDbSet<BlogPostLocation> BlogPostLocations { get; set; }

        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public IDbSet<File> Files { get; set; }
        public IDbSet<BinaryData> BinaryData { get; set; }

        public IDbSet<Location> Locations { get; set; }

        public IDbSet<CarouselItem> CarouselItems { get; set; }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Testimonial> Testimonials { get; set; }

        //public IDbSet<Customer> Customers { get; set; }
        //public IDbSet<Opportunity> Opportunities { get; set; }
        //public IDbSet<Risk> Risks { get; set; }








        //1 - * - Always do one way nagivation properties and put foreign key in the 1 to many class
        //Just set Foreign key property. e.g ChildClass.ParentID = 5 rather than ChildClass.Parent = Parent
        //Many - to - Many - ICollection in each entity will automatically create link table. Bad Idea

        // 1: 1 or 1:0...1 - Bidrectional navigation properties but remove foreign key property. Configuration to determine Principal and Dependent end of relationship
        //modelBuilder.Entity<Customer>().HasOptional(c => c.ContactDetail).WithRequired(d => d.Customer);
        //ContactDetail has required Customer with optional ContactDetail

        //Instead of 1 : 1 extend from ValueObject


        //The SetInitializer() method takes an instance of database initializer class and sets it as a database initializer for the current application domain.When you set a database initializer, it won't be called immediately. It will be called when the context (BlogContext) is used for the first time. In the preceding example, the actual database creation will occur only when you add a new Category and BlogPost and not when a new instance of BlogContext is created.
        //        Database.SetInitializer(new DropCreateDatabaseAlways<BlogContext>());
        //using (var db = new BlogContext())
        //{
        //    db.Database.Initialize(false);
        //    ...
        //}


        //In the above code snippet you are calling the Initialize() method immediately after creating a context instance.In this case, the database will be created immediately after calling the Initialize() method instead of waiting until the context is used for the first time. The Initialize() method takes a boolean parameter that controls whether the initialization process should re-run if it has already run for the application. Specifying false will skip the initialization process if it has already executed. A value of true will initialize the database again even if it was already initialized.

        //At times you may want to use an existing database with Code First. In such cases you may not want to execute any initialization logic at all. You can suppress the database initialization process altogether by passing null to SetInitializer() method.
        public ApplicationDbContext()
            : base("name=DefaultConnectionString")
        {
            
            this.Database.CommandTimeout = 180;
            //SqlProviderServices.SqlServerTypesAssemblyName = "Microsoft.SqlServer.Types, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
            //Once a migration is created DB is never created
            //Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext,ApplicationDbConfiguration>());
            //Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializerTest());
            //DbContextInitializer<ApplicationDbContext>.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ApplicationDbConfiguration>());
            SetInitializer(new ApplicationDbInitializerMigrate());
            //Database.SetInitializer<YourContext>(new CreateDatabaseIfNotExists<YourContext>()); //Default one
            //Database.SetInitializer<YourContext>(new DropCreateDatabaseIfModelChanges<YourContext>()); //Drop database if changes detected
            //Database.SetInitializer<YourContext>(new DropCreateDatabaseAlways<YourContext>()); //Drop database every times
            //Database.SetInitializer<YourContext>(new CustomInitializer<YourContext>()); //Custom if model changed and seed values

        }

        static ApplicationDbContext()
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new GigConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");

            //modelBuilder.Entity<Airport>().ToTable("Airport");
            //modelBuilder.Entity<City>().ToTable("City");
            //modelBuilder.Entity<Country>().ToTable("Country");
        }
    }
}
