namespace GladiatorProject.Migrations
{
    using GladiatorProject.Models;
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

            //context.ClassRoles.AddOrUpdate(
            //    c => c.Name,
            //    new ClassRole { Name = "Murmillo", Weapon = "Sword and Heavy Shield", Armor = 5, Damage = 5, Health = 20 },
            //    new ClassRole { Name = "Retiarius", Weapon = "Trident and Net", Armor = 2, Damage = 9, Health = 20 },
            //    new ClassRole { Name = "Dimachaerus", Weapon = "Sword and Sword", Armor = 3, Damage = 7, Health = 20 },
            //    new ClassRole { Name = "Cestus", Weapon = "knuckleduster", Armor = 1, Damage = 10, Health = 20 }
            //    );

            context.Opponents.AddOrUpdate(
                o => o.Name,
                new Opponent { Name = "Murmillo", Health = 10 + Dice.D12(), Armor = 2 + Dice.D8(), Damage = 1 + Dice.D6(), Level = 1 },
                new Opponent { Name = "Retiarius", Health = 10 + Dice.D12(), Armor = 2 + Dice.D8(), Damage = 1 + Dice.D6(), Level = 1 },
                new Opponent { Name = "Dimachaerus", Health = 10 + Dice.D12(), Armor = 2 + Dice.D8(), Damage = 1 + Dice.D6(), Level = 1 },
                new Opponent { Name = "Cestus", Health = 10 + Dice.D12(), Armor = 2 + Dice.D8(), Damage = 1 + Dice.D6(), Level = 1 }
                );

            context.SaveChanges();
           
        }
    }
}
