namespace Will_Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Will_Blog.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Will_Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Will_Blog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            #region roleManager

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if(!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            #endregion

            #region Assign User Roles

            //create users that will occupy roles of either admin or moderator

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            if(!context.Users.Any(u => u.Email == "JTwichell@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JTwichell@mailinator.com",
                    Email = "JTwichell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "twich"

                }, "abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "AWill@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "AWill@mailinator.com",
                    Email = "AWill@mailinator.com",
                    FirstName = "Will",
                    LastName = "Laspata",
                    DisplayName = "WillPasta"

                }, "abc&123!");
            }
            // how to assign user and moderator

            var userId = userManager.FindByEmail("AWill@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("JTwichell@mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");


            #endregion Assign User Roles
        }
    }
}
