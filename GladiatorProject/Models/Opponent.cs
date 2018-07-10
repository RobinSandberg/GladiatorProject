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

        [Required]
        [MaxLength(50)] 
        public string Name { get; set; }

        public int Health { get; set; }

        public int FullHealth { get; set; }

        public int Armor { get; set; }

        public string DamageDice { get; set; }

        public int Strenght { get; set; }

        public int StrenghtModifyer { get; set; }

        public int Constitution { get; set; }

        public int ConstitutionModifyer { get; set; }

        [Required]
        public int Level { get; set; }

        public List<Opponent> Levels = new List<Opponent>();  // List of the opponents in range of gladiator level.

        public static void EnemyStats(Opponent enemy) // the stats added to opponents that got 0 health based on their level.
        {
           
            switch (enemy.Level)
            {
                case 1:
                    enemy.Armor = 8 + Dice.D6();
                    enemy.Constitution = 5 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 5 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 10 + (Dice.D10() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 2:
                    enemy.Armor = 8 + Dice.D6();
                    enemy.Constitution = 6 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 6 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 11 + (Dice.D10() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 3:
                    enemy.Armor = 8 + Dice.D6();
                    enemy.Constitution = 7 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 7 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 12 + (Dice.D10() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 4:
                    enemy.Armor = 9 + Dice.D6();
                    enemy.Constitution = 8 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 8 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth =  13 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 5:
                    enemy.Armor = 9 + Dice.D6();
                    enemy.Constitution = 9 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 9 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 14 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth; 
                    enemy.DamageDice = "2xD8";
                    break;
                case 6:
                    enemy.Armor = 9 + Dice.D6();
                    enemy.Constitution = 10 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 10 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 15 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth; 
                    enemy.DamageDice = "2xD8";
                    break;
                case 7:
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Constitution = 11 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 11 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 16 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 8:
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Constitution = 12 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 12 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 17 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 9:
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Constitution = 13 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 13 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 18 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 10:
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Constitution = 14 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 14 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 19 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + 
                        Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 11:
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Constitution = 15 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 15 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 20 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 12:
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Constitution = 16 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 16 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 21 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 13:
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Constitution = 17 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 17 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 22 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 14:
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Constitution = 18 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 18 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 23 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 15:
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Constitution = 19 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 19 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 24 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 16:
                    enemy.Armor = 13 + Dice.D4();
                    enemy.Constitution = 20 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 20 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 25 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 17:
                    enemy.Armor = 13 + Dice.D4();
                    enemy.Constitution = 21 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 21 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 26 + (Dice.D10() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) +
                        Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() );
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 18:
                    enemy.Armor = 15 + Dice.D4();
                    enemy.Constitution = 22 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 22 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 27 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 19:
                    enemy.Armor = 16 + Dice.D4();
                    enemy.Constitution = 23 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 23 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 28 + (Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() +
                        Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                case 20:
                    enemy.Armor = 16 + Dice.D4();
                    enemy.Constitution = 24 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 24 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = 29 + (Dice.D10() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) +
                        Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) + Dice.D6() + (Dice.D6() + enemy.ConstitutionModifyer) +
                        Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer);
                    enemy.Health = enemy.FullHealth;
                    enemy.DamageDice = "2xD8";
                    break;
                    
            }
            enemy.FullHealth = enemy.Health;
        }
    }
}