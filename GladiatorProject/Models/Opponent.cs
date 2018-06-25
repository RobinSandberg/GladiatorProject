using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lexicon.CSharp.InfoGenerator;
using GladiatorProject.Models;
using System.ComponentModel.DataAnnotations;

namespace GladiatorProject.Models
{
    
    public class Opponent
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]    
        public string Name { get; set; }

        public int Health { get; set; }

        public int FullHealth { get; set; }

        public int Armor { get; set; }

        public int Damage { get; set; }

        public int Level { get; set; }

        public List<int> Levels = new List<int>();


        public static void EnemyStats(Opponent enemy)
        //public Opponent(Opponent enemy)
        {
            InfoGenerator names = new InfoGenerator(DateTime.Now.Millisecond);
            Gender gender = Gender.Any;
            
            switch (enemy.Level)
            {
                case 1:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 10 + Dice.D12();
                    enemy.Armor = 10 + Dice.D4();
                    enemy.Damage = 1 + Dice.D6();
                    break;
                case 2:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 11 + Dice.D12();
                    enemy.Armor = 10 + Dice.D4();
                    enemy.Damage = 2 + Dice.D6();
                    break;
                case 3:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 12 + Dice.D12();
                    enemy.Armor = 10 + Dice.D4();
                    enemy.Damage = 3 + Dice.D6();
                    break;
                case 4:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 13 + Dice.D12();
                    enemy.Armor = 10 + Dice.D4();
                    enemy.Damage = 4 + Dice.D6();
                    break;
                case 5:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 14 + Dice.D12();
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Damage = 5 + Dice.D6();
                    break;
                case 6:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 15 + Dice.D12();
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Damage = 6 + Dice.D6();
                    break;
                case 7:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 16 + Dice.D12();
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Damage = 7 + Dice.D6();
                    break;
                case 8:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 17 + Dice.D12();
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Damage = 8 + Dice.D6();
                    break;
                case 9:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 18 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 9 + Dice.D6();
                    break;
                case 10:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 19 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 10 + Dice.D6();
                    break;
                case 11:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 20 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 11 + Dice.D6();
                    break;
                case 12:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 21 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 12 + Dice.D6();
                    break;
                case 13:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 22 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 13 + Dice.D6();
                    break;
                case 14:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 23 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 14 + Dice.D6();
                    break;
                case 15:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 24 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 15 + Dice.D6();
                    break;
                case 16:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 25 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 16 + Dice.D6();
                    break;
                case 17:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 26 + Dice.D12();
                    enemy.Armor = 14 + Dice.D4();
                    enemy.Damage = 17 + Dice.D6();
                    break;
                case 18:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 27 + Dice.D12();
                    enemy.Armor = 14 + Dice.D4();
                    enemy.Damage = 18 + Dice.D6();
                    break;
                case 19:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 28 + Dice.D12();
                    enemy.Armor = 14 + Dice.D4();
                    enemy.Damage = 19 + Dice.D6();
                    break;
                case 20:
                    enemy.Name = names.NextFullName(gender);
                    enemy.Health = 29 + Dice.D12();
                    enemy.Armor = 15 + Dice.D4();
                    enemy.Damage = 20 + Dice.D6();
                    break;
                    
            }
            enemy.FullHealth = enemy.Health;
        }
    }
}