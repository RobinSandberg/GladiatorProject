namespace GladiatorProject.Migrations
{
    using GladiatorProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GladiatorProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GladiatorProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("Overlord"))
            {
                roleManager.Create(new IdentityRole("Overlord"));
            }

            if (!roleManager.RoleExists("Player"))
            {
                roleManager.Create(new IdentityRole("Player"));
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.FindByEmail("Overlord@Admin.se") == null)
            {
                ApplicationUser Overlord = new ApplicationUser()
                {
                    Email = "Overlord@Admin.se",
                    UserName = "Overlord@Admin.se"
                };
                userManager.Create(Overlord, "As!1234");
                userManager.AddToRole(userManager.FindByEmail("Overlord@Admin.se").Id, "Overlord");
            }

            if (userManager.FindByEmail("Player@Normal.se") == null)
            {
                ApplicationUser Player = new ApplicationUser()
                {
                    Email = "Player@Normal.se",
                    UserName = "Player@Normal.se"
                };
                userManager.Create(Player, "As!1234");
                userManager.AddToRole(userManager.FindByEmail("Player@Normal.se").Id, "Player");
            }

            //context.ClassRoles.AddOrUpdate(
            //    c => c.Name,
            //    new ClassRole { Name = "Murmillo", Weapon = "Sword and Heavy Shield", Armor = 5, Damage = 5, Health = 20 },
            //    new ClassRole { Name = "Retiarius", Weapon = "Trident and Net", Armor = 2, Damage = 9, Health = 20 },
            //    new ClassRole { Name = "Dimachaerus", Weapon = "Sword and Sword", Armor = 3, Damage = 7, Health = 20 },
            //    new ClassRole { Name = "Cestus", Weapon = "knuckleduster", Armor = 1, Damage = 10, Health = 20 }
            //    );


            context.SaveChanges();
           
        }
    }
}
