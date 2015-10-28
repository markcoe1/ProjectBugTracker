namespace ProjectBugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjectBugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectBugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ProjectBugTracker.Models.ApplicationDbContext";
        }

        protected override void Seed(ProjectBugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                            new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "glaubermancoe@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "glaubermancoe@gmail.com",
                    Email = "glaubermancoe@gmail.com",
                    FirstName = "Mark",
                    LastName = "Coe",
                    DisplayName = "Admin",


                }, "4zmwbp94");
            }

            roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }
            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "araynor@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "araynor@coderfoundry.com",
                    Email = "araynor@coderfoundry.com",
                    FirstName = "Antonio",
                    LastName = "Raynor",
                    DisplayName = "ARaynor",


                }, "Abc&123!");
            }
            roleManager = new RoleManager<IdentityRole>(
             new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "bdavis@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "bdavis@coderfoundry.com",
                    Email = "bdavis@coderfoundry.com",
                    FirstName = "Bobby",
                    LastName = "Davis",
                    DisplayName = "BDavis",


                }, "Abc&123!");
            }
            roleManager = new RoleManager<IdentityRole>(
       new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "ajensen@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ajensen@coderfoundry.com",
                    Email = "ajensen@coderfoundry.com",
                    FirstName = "Andrew",
                    LastName = "Jensen",
                    DisplayName = "AJensen",


                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("glaubermancoe@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");
            userId = userManager.FindByEmail("araynor@coderfoundry.com").Id;
            userManager.AddToRole(userId, "ProjectManager");
            userId = userManager.FindByEmail("bdavis@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Developer");
            userId = userManager.FindByEmail("ajensen@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Submitter");
        }
    }
}
