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

            context.Classes.AddOrUpdate(
                c => c.Name,
                new ClassRole { Name = "Murmillo", Weapon = "Sword and Heavy Shield", Armor = 5 + Dice.D6(), Damage = 5 + Dice.D6(), Health = 20 + Dice.D12() },
                new ClassRole { Name = "Retiarius", Weapon = "Trident and Net", Armor = 2 + Dice.D4(), Damage = 9 + Dice.D8(), Health = 20 + Dice.D8() },
                new ClassRole { Name = "Dimachaerus", Weapon = "Sword and Sword", Armor = 3 + Dice.D6(), Damage = 7 + Dice.D10(), Health = 20 + Dice.D6() },
                new ClassRole { Name = "Cestus", Weapon = "knuckleduster", Armor = 1 + Dice.D6(), Damage = 10 + Dice.D12(), Health = 20 + Dice.D10() }
                );

            context.Opponents.AddOrUpdate(
                o => o.Name,
                new Opponent { Name = "Male", Class = "Murmillo", Health = 20 + Dice.D12(), Armor = 5 + Dice.D6(), Damage = 5 + Dice.D6(), Level = 1 },
                new Opponent { Name = "Male", Class = "Retiarius", Health = 20 + Dice.D8(), Armor = 1 + Dice.D4(), Damage = 9 + Dice.D8(), Level = 1 },
                new Opponent { Name = "Male", Class = "Dimachaerus", Health = 20 + Dice.D6(), Armor = 3 + Dice.D6(), Damage = 7 + Dice.D10(), Level = 1 },
                new Opponent { Name = "Male", Class = "Cestus", Health = 20 + Dice.D10(), Armor = 1 + Dice.D6(), Damage = 10 + Dice.D12(), Level = 1 },

                new Opponent { Name = "Female", Class = "Murmillo", Health = 20 + Dice.D12(), Armor = 5 + Dice.D6(), Damage = 5 + Dice.D6(), Level = 1 },
                new Opponent { Name = "Female", Class = "Retiarius", Health = 20 + Dice.D8(), Armor = 1 + Dice.D4(), Damage = 9 + Dice.D8(), Level = 1 },
                new Opponent { Name = "Female", Class = "Dimachaerus", Health = 20 + Dice.D6(), Armor = 3 + Dice.D6(), Damage = 7 + Dice.D10(), Level = 1 },
                new Opponent { Name = "Female", Class = "Cestus", Health = 20 + Dice.D10(), Armor = 1 + Dice.D6(), Damage = 10 + Dice.D12(), Level = 1 }
                );
        }
    }
}
