using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND.EFPersistance;
using DND.Core.Model;

using Solution.Base.Extensions;
using Solution.Base.Implementation.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using DND.Core.Constants;

namespace DND.EFPersistance.Initializers
{
    public class DBSeed
    {
        public static void Seed(ApplicationDbContext context)
        {
            AddRoles(context);
            AddUsers(context);
            AddContentHtml(context);
            context.SaveChanges();
        }

        private static void AddRoles(ApplicationDbContext context)
        {
            string[] roles = new string[] { "admin" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }
        }

        private static void AddUsers(ApplicationDbContext context)
        {
            if (!(context.Users.Any(u => u.UserName == "admin")))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "admin", Name = "admin" };
                userManager.Create(userToInsert, "password");

                var user = userManager.FindByName("admin");
                userManager.AddToRole(user.Id, "admin");
            }
        }

        private static void AddContentText(ApplicationDbContext context)
        {
            string[] roles = new string[] { "admin" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }
        }

        private static void AddContentHtml(ApplicationDbContext context)
        {
            AddContentHTML(context, CMS.ContentHtml.About, "<p>About Me</p>");
            AddContentHTML(context, CMS.ContentHtml.SideBarAbout, "<p>About Me</p>");
            AddContentHTML(context, CMS.ContentHtml.WorkWithMe, "<p>Work With Me</p>");
            AddContentHTML(context, CMS.ContentHtml.Affiliates, "<p>Affiliates</p>");
            AddContentHTML(context, CMS.ContentHtml.Resume, "<p>Resume</p>");
            AddContentHTML(context, Solution.Base.Constants.CMS.ContentHtml.Contact, "<p>Contact</p>");
            AddContentHTML(context, Solution.Base.Constants.CMS.ContentHtml.Head, "");
            AddContentHTML(context, Solution.Base.Constants.CMS.ContentHtml.Main, "");
        }

        private static void AddContentHTML(ApplicationDbContext context, string id, string content)
        {
            if (!context.ContentHtml.Any(c => c.Id == id))
            {
                context.ContentHtml.Add(new ContentHtml() { Id = id, HTML = content, PreventDelete = true, DateCreated = DateTime.Now, UserCreated = "SYSTEM" });
            }
        }
    }
}
