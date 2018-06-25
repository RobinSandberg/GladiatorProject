using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace GladiatorProject.Models
{
    public class Gladiator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public int Health { get; set; }

        public int FullHealth { get; set; }

        public int Armor { get; set; }

        public int Damage { get; set; }

        public int SkillPoints { get; set; }

        public int Experiance { get; set; }

        public int Level { get; set; }

        public int Gold { get; set; }

        public int Battles { get; set; }

        public int BattlesWon { get; set; }

        public int BattlesLost { get; set; }

        public int BattlesDraw { get; set; }

        public List<Opponent> DefeatedOpponent = new List<Opponent>();
        //public Opponent Opponent { get; set; }

        public Gladiator()
        {
            Health = 10 + Dice.D12();
            FullHealth = Health;
            Armor = 10 + Dice.D4();
            Damage = 1 + Dice.D6();
            Experiance = 0;
            Level = 1;
            SkillPoints = 0;
            Gold = 20;
            Battles = 0;
            BattlesWon = 0;
            BattlesDraw = 0;
            BattlesLost = 0;
        }

        public static void Leveling(Gladiator RankUp)
        {
            switch (RankUp.Level)
            {
                case 1:
                    if (RankUp.Experiance >= 100)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 2:
                    if (RankUp.Experiance >= 250)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 3:
                    if (RankUp.Experiance >= 450)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 4:
                    if (RankUp.Experiance >= 700)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 5:
                    if (RankUp.Experiance >= 1000)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 6:
                    if (RankUp.Experiance >= 1350)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 7:
                    if (RankUp.Experiance >= 1750)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 8:
                    if (RankUp.Experiance >= 2200)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 9:
                    if (RankUp.Experiance >= 2700)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 10:
                    if (RankUp.Experiance >= 3250)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 11:
                    if (RankUp.Experiance >= 3850)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 12:
                    if (RankUp.Experiance >= 4500)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 13:
                    if (RankUp.Experiance >= 5200)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 14:
                    if (RankUp.Experiance >= 5950)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 15:
                    if (RankUp.Experiance >= 6750)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 16:
                    if (RankUp.Experiance >= 7600)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 17:
                    if (RankUp.Experiance >= 8500)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 18:
                    if (RankUp.Experiance >= 9450)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 19:
                    if (RankUp.Experiance >= 10450)
                    {
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
            }
        }

        public static void Healing(Gladiator Heal)
        {
            int cost = (Heal.FullHealth - Heal.Health) / 2;
           
            if (cost > Heal.Gold)
            {
                int test = cost - Heal.Gold;
                Heal.Health = Heal.FullHealth - (test * 2);
                Heal.Gold = 0;
            }
            else
            {
                Heal.Health = Heal.FullHealth;
                Heal.Gold -= cost;
            } 
            
        }

        public static void AddingStats(Gladiator Stats , string stat)
        {
            switch (stat)
            {
                case "Health":
                    Stats.Health += 1;
                    Stats.FullHealth += 1;
                    Stats.SkillPoints -= 1;
                        break;
                case "Armor":
                    if(Stats.Armor < 18)
                    {
                        Stats.Armor += 1;
                        Stats.SkillPoints -= 1;
                    }
                    break;
                case "Damage":
                    Stats.Damage += 1;
                    Stats.SkillPoints -= 1;
                    break;
            }
        }
    }
}
