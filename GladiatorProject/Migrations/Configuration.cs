namespace GladiatorProject.Migrations
{
    using GladiatorProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Lexicon.CSharp.InfoGenerator;

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
                roleManager.Create(new IdentityRole("Overlord")); //Creating the admin role if it don't exist.
            }

            if (!roleManager.RoleExists("Player"))
            {
                roleManager.Create(new IdentityRole("Player"));  // Creating the player role if it don't exist.
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.FindByEmail("Overlord@Admin.se") == null)
            {
                ApplicationUser Overlord = new ApplicationUser()
                {
                    Email = "Overlord@Admin.se",
                    UserName = "Overlord"
                };
                userManager.Create(Overlord, "As!1234");
                userManager.AddToRole(userManager.FindByEmail("Overlord@Admin.se").Id, "Overlord");  // Creating the admin name and password and adding him to the role.
            }

            if (userManager.FindByEmail("Player@Normal.se") == null)
            {
                ApplicationUser Player = new ApplicationUser()
                {
                    Email = "Player@Normal.se",
                    UserName = "Player"
                };
                userManager.Create(Player, "As!1234");
                userManager.AddToRole(userManager.FindByEmail("Player@Normal.se").Id, "Player"); // Creating a player name and password and adding him to the role.
            }

            //context.ClassRoles.AddOrUpdate(
            //    c => c.Name,
            //    new ClassRole { Name = "Murmillo", Weapon = "Sword and Heavy Shield", Armor = 5, Damage = 5, Health = 20 },
            //    new ClassRole { Name = "Retiarius", Weapon = "Trident and Net", Armor = 2, Damage = 9, Health = 20 },
            //    new ClassRole { Name = "Dimachaerus", Weapon = "Sword and Sword", Armor = 3, Damage = 7, Health = 20 },
            //    new ClassRole { Name = "Cestus", Weapon = "knuckleduster", Armor = 1, Damage = 10, Health = 20 }
            //    );

            InfoGenerator names = new InfoGenerator(DateTime.Now.Millisecond);
            Gender gender = Gender.Any;
            // Adding a list of 100 opponents from lvl 1 to 20 with random names to database.
            context.Opponents.AddOrUpdate(
                i => i.Name,
                new Opponent { Name = names.NextFullName(gender), Level = 1 },
                new Opponent { Name = names.NextFullName(gender), Level = 1 },
                new Opponent { Name = names.NextFullName(gender), Level = 1 },
                new Opponent { Name = names.NextFullName(gender), Level = 1 },
                new Opponent { Name = names.NextFullName(gender), Level = 1 },

                new Opponent { Name = names.NextFullName(gender), Level = 2 },
                new Opponent { Name = names.NextFullName(gender), Level = 2 },
                new Opponent { Name = names.NextFullName(gender), Level = 2 },
                new Opponent { Name = names.NextFullName(gender), Level = 2 },
                new Opponent { Name = names.NextFullName(gender), Level = 2 },

                new Opponent { Name = names.NextFullName(gender), Level = 3 },
                new Opponent { Name = names.NextFullName(gender), Level = 3 },
                new Opponent { Name = names.NextFullName(gender), Level = 3 },
                new Opponent { Name = names.NextFullName(gender), Level = 3 },
                new Opponent { Name = names.NextFullName(gender), Level = 3 },

                new Opponent { Name = names.NextFullName(gender), Level = 4 },
                new Opponent { Name = names.NextFullName(gender), Level = 4 },
                new Opponent { Name = names.NextFullName(gender), Level = 4 },
                new Opponent { Name = names.NextFullName(gender), Level = 4 },
                new Opponent { Name = names.NextFullName(gender), Level = 4 },

                new Opponent { Name = names.NextFullName(gender), Level = 5 },
                new Opponent { Name = names.NextFullName(gender), Level = 5 },
                new Opponent { Name = names.NextFullName(gender), Level = 5 },
                new Opponent { Name = names.NextFullName(gender), Level = 5 },
                new Opponent { Name = names.NextFullName(gender), Level = 5 },

                new Opponent { Name = names.NextFullName(gender), Level = 6 },
                new Opponent { Name = names.NextFullName(gender), Level = 6 },
                new Opponent { Name = names.NextFullName(gender), Level = 6 },
                new Opponent { Name = names.NextFullName(gender), Level = 6 },
                new Opponent { Name = names.NextFullName(gender), Level = 6 },

                new Opponent { Name = names.NextFullName(gender), Level = 7 },
                new Opponent { Name = names.NextFullName(gender), Level = 7 },
                new Opponent { Name = names.NextFullName(gender), Level = 7 },
                new Opponent { Name = names.NextFullName(gender), Level = 7 },
                new Opponent { Name = names.NextFullName(gender), Level = 7 },

                new Opponent { Name = names.NextFullName(gender), Level = 8 },
                new Opponent { Name = names.NextFullName(gender), Level = 8 },
                new Opponent { Name = names.NextFullName(gender), Level = 8 },
                new Opponent { Name = names.NextFullName(gender), Level = 8 },
                new Opponent { Name = names.NextFullName(gender), Level = 8 },

                new Opponent { Name = names.NextFullName(gender), Level = 9 },
                new Opponent { Name = names.NextFullName(gender), Level = 9 },
                new Opponent { Name = names.NextFullName(gender), Level = 9 },
                new Opponent { Name = names.NextFullName(gender), Level = 9 },
                new Opponent { Name = names.NextFullName(gender), Level = 9 },

                new Opponent { Name = names.NextFullName(gender), Level = 10 },
                new Opponent { Name = names.NextFullName(gender), Level = 10 },
                new Opponent { Name = names.NextFullName(gender), Level = 10 },
                new Opponent { Name = names.NextFullName(gender), Level = 10 },
                new Opponent { Name = names.NextFullName(gender), Level = 10 },

                new Opponent { Name = names.NextFullName(gender), Level = 11 },
                new Opponent { Name = names.NextFullName(gender), Level = 11 },
                new Opponent { Name = names.NextFullName(gender), Level = 11 },
                new Opponent { Name = names.NextFullName(gender), Level = 11 },
                new Opponent { Name = names.NextFullName(gender), Level = 11 },

                new Opponent { Name = names.NextFullName(gender), Level = 12 },
                new Opponent { Name = names.NextFullName(gender), Level = 12 },
                new Opponent { Name = names.NextFullName(gender), Level = 12 },
                new Opponent { Name = names.NextFullName(gender), Level = 12 },
                new Opponent { Name = names.NextFullName(gender), Level = 12 },

                new Opponent { Name = names.NextFullName(gender), Level = 13 },
                new Opponent { Name = names.NextFullName(gender), Level = 13 },
                new Opponent { Name = names.NextFullName(gender), Level = 13 },
                new Opponent { Name = names.NextFullName(gender), Level = 13 },
                new Opponent { Name = names.NextFullName(gender), Level = 13 },

                new Opponent { Name = names.NextFullName(gender), Level = 14 },
                new Opponent { Name = names.NextFullName(gender), Level = 14 },
                new Opponent { Name = names.NextFullName(gender), Level = 14 },
                new Opponent { Name = names.NextFullName(gender), Level = 14 },
                new Opponent { Name = names.NextFullName(gender), Level = 14 },

                new Opponent { Name = names.NextFullName(gender), Level = 15 },
                new Opponent { Name = names.NextFullName(gender), Level = 15 },
                new Opponent { Name = names.NextFullName(gender), Level = 15 },
                new Opponent { Name = names.NextFullName(gender), Level = 15 },
                new Opponent { Name = names.NextFullName(gender), Level = 15 },

                new Opponent { Name = names.NextFullName(gender), Level = 16 },
                new Opponent { Name = names.NextFullName(gender), Level = 16 },
                new Opponent { Name = names.NextFullName(gender), Level = 16 },
                new Opponent { Name = names.NextFullName(gender), Level = 16 },
                new Opponent { Name = names.NextFullName(gender), Level = 16 },

                new Opponent { Name = names.NextFullName(gender), Level = 17 },
                new Opponent { Name = names.NextFullName(gender), Level = 17 },
                new Opponent { Name = names.NextFullName(gender), Level = 17 },
                new Opponent { Name = names.NextFullName(gender), Level = 17 },
                new Opponent { Name = names.NextFullName(gender), Level = 17 },

                new Opponent { Name = names.NextFullName(gender), Level = 18 },
                new Opponent { Name = names.NextFullName(gender), Level = 18 },
                new Opponent { Name = names.NextFullName(gender), Level = 18 },
                new Opponent { Name = names.NextFullName(gender), Level = 18 },
                new Opponent { Name = names.NextFullName(gender), Level = 18 },

                new Opponent { Name = names.NextFullName(gender), Level = 19 },
                new Opponent { Name = names.NextFullName(gender), Level = 19 },
                new Opponent { Name = names.NextFullName(gender), Level = 19 },
                new Opponent { Name = names.NextFullName(gender), Level = 19 },
                new Opponent { Name = names.NextFullName(gender), Level = 19 },

                new Opponent { Name = names.NextFullName(gender), Level = 20 },
                new Opponent { Name = names.NextFullName(gender), Level = 20 },
                new Opponent { Name = names.NextFullName(gender), Level = 20 },
                new Opponent { Name = names.NextFullName(gender), Level = 20 },
                new Opponent { Name = names.NextFullName(gender), Level = 20 }
                );

            context.SaveChanges();
           
        }
    }
}
