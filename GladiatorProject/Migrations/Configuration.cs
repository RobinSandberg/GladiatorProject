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
                    UserName = "Overlord82"
                };
                userManager.Create(Overlord, "Qwe!23");
                userManager.AddToRole(userManager.FindByEmail("Overlord@Admin.se").Id, "Overlord");
            }

            if (userManager.FindByEmail("Player@Normal.se") == null)
            {
                ApplicationUser Player = new ApplicationUser()
                {
                    Email = "Player@Normal.se",
                    UserName = "Playerone"
                };
                userManager.Create(Player, "Qwe!23");
                userManager.AddToRole(userManager.FindByEmail("Player@Normal.se").Id, "Player");
            }

            //context.ClassRoles.AddOrUpdate(
            //    c => c.Name,
            //    new ClassRole { Name = "Murmillo", Weapon = "Sword and Heavy Shield", Armor = 5, Damage = 5, Health = 20 },
            //    new ClassRole { Name = "Retiarius", Weapon = "Trident and Net", Armor = 2, Damage = 9, Health = 20 },
            //    new ClassRole { Name = "Dimachaerus", Weapon = "Sword and Sword", Armor = 3, Damage = 7, Health = 20 },
            //    new ClassRole { Name = "Cestus", Weapon = "knuckleduster", Armor = 1, Damage = 10, Health = 20 }
            //    );


            context.Opponents.AddOrUpdate(
                o => o.Name,
                //new Opponent { Name = "Murmillo", Health = 10 + Dice.D12(), Armor = 2 + Dice.D8(), Damage = 1 + Dice.D6(), Level = 1 },
                //new Opponent { Name = "Retiarius", Health = 11 + Dice.D12(), Armor = 3 + Dice.D8(), Damage = 2 + Dice.D6(), Level = 2 },
                //new Opponent { Name = "Dimachaerus", Health = 12 + Dice.D12(), Armor = 3 + Dice.D8(), Damage = 3 + Dice.D6(), Level = 3 },
                //new Opponent { Name = "Cestus", Health = 13 + Dice.D12(), Armor = 4 + Dice.D8(), Damage = 4 + Dice.D6(), Level = 4 }
                new Opponent { Name = "Torin Demot", Level = 1 },
                new Opponent { Name = "Ista Torro", Level = 2 },
                new Opponent { Name = "Eor verin", Level = 3 },
                new Opponent { Name = "Orix nidu", Level = 4 },
                new Opponent { Name = "Vix Hursy", Level = 5 },
                new Opponent { Name = "Zerti Jorg", Level = 6 },
                new Opponent { Name = "Aino Dimi", Level = 7 },
                new Opponent { Name = "Serno ajol", Level = 8 },
                new Opponent { Name = "Cora Lorin", Level = 9 },
                new Opponent { Name = "Rigon fezin", Level = 10},
                new Opponent { Name = "Niryn Urz", Level = 11},
                new Opponent { Name = "Kasti Gort", Level = 12},
                new Opponent { Name = "Bintiz Yvor", Level = 13},
                new Opponent { Name = "Pixnos Aluria", Level = 14},
                new Opponent { Name = "Mogor Hidus", Level = 15},
                new Opponent { Name = "Vetro Egonos", Level = 16},
                new Opponent { Name = "Lezin Denom", Level = 17},
                new Opponent { Name = "Cipo Sindol", Level = 18},
                new Opponent { Name = "Gono uztila", Level = 19},
                new Opponent { Name = "Robin Sandberg", Level = 20 }

                );

            context.SaveChanges();
           
        }
    }
}
