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

        public int MaxArmor { get; set; }

        public int Damage { get; set; }

        public string DamageDice { get; set; }

        public int Strenght { get; set; }

        public int StrenghtModifyer { get; set; }

        public int Constitution { get; set; }

        public int ConstitutionModifyer { get; set; }

        public int SkillPoints { get; set; }

        public int Experiance { get; set; }

        public int Level { get; set; }

        public int Gold { get; set; }

        public int Battles { get; set; }

        public int BattlesWon { get; set; }

        public int BattlesLost { get; set; }

        public int BattlesDraw { get; set; }

        public int CurrentWinningStreak { get; set; }

        public int BestWinningStreak { get; set; }

        public string LastBattle { get; set; }
        //public Opponent Opponent { get; set; }

        //public Gladiator()
        //{
        //   Health = Dice.D10();
        //   FullHealth = Health;
        //   Armor = Dice.D6() + Dice.D6() + Dice.D6();
        //   MaxArmor = 18;
        //   Strenght = 0;
        //   StrenghtModifyer = (Strenght - 10) / 2;
        //   Constitution = 0;
        //   ConstitutionModifyer = (Constitution - 10) / 2;
        //   Damage = StrenghtModifyer;
        //   Experiance = 0;
        //   Level = 1;
        //   SkillPoints = 20;
        //   Gold = 20;
        //   Battles = 0;
        //   BattlesWon = 0;
        //   BattlesDraw = 0;
        //   BattlesLost = 0;
        //   CurrentWinningStreak = 0;
        //   BestWinningStreak = 0;
        //   LastBattle = "";   
        //}

        public static void StartingGladiator(Gladiator start)
        {
            start.Health = Dice.D10();
            start.FullHealth = start.Health;
            start.Armor = Dice.D6() + Dice.D6() + Dice.D6();
            start.MaxArmor = 18;
            start.Strenght = Dice.D6() + Dice.D6();
            start.StrenghtModifyer = (start.Strenght - 10) / 2;
            start.Constitution = Dice.D6() + Dice.D6();
            start.ConstitutionModifyer = (start.Constitution - 10) / 2;
            start.Damage = start.StrenghtModifyer;
            start.DamageDice = "2xD8";
            start.Experiance = 0;
            start.Level = 1;
            start.SkillPoints = 10;
            start.Gold = 20;
            start.Battles = 0;
            start.BattlesWon = 0;
            start.BattlesDraw = 0;
            start.BattlesLost = 0;
            start.CurrentWinningStreak = 0;
            start.BestWinningStreak = 0;
            start.LastBattle = "";
        }

        public static void Leveling(Gladiator RankUp)
        {
            int HealthAdd = Dice.D6() + RankUp.ConstitutionModifyer;
            switch (RankUp.Level)
            {
                case 1:
                    if (RankUp.Experiance >= 100)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 2:
                    if (RankUp.Experiance >= 250)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 3:
                    if (RankUp.Experiance >= 450)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 4:
                    if (RankUp.Experiance >= 700)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 5:
                    if (RankUp.Experiance >= 1000)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 6:
                    if (RankUp.Experiance >= 1350)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 7:
                    if (RankUp.Experiance >= 1750)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 8:
                    if (RankUp.Experiance >= 2200)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 9:
                    if (RankUp.Experiance >= 2700)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 10:
                    if (RankUp.Experiance >= 3250)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 11:
                    if (RankUp.Experiance >= 3850)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 12:
                    if (RankUp.Experiance >= 4500)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 13:
                    if (RankUp.Experiance >= 5200)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 14:
                    if (RankUp.Experiance >= 5950)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 15:
                    if (RankUp.Experiance >= 6750)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 16:
                    if (RankUp.Experiance >= 7600)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 17:
                    if (RankUp.Experiance >= 8500)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 18:
                    if (RankUp.Experiance >= 9450)
                    {
                        RankUp.FullHealth += HealthAdd;
                        RankUp.Level += 1;
                        RankUp.SkillPoints += 2;
                    }
                    break;
                case 19:
                    if (RankUp.Experiance >= 10450)
                    {
                        RankUp.FullHealth += HealthAdd;
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
                case "Constitution":
                    Stats.Constitution += 1;
                    Stats.ConstitutionModifyer = (Stats.Constitution - 10) / 2;
                    Stats.FullHealth += 1;
                    Stats.SkillPoints -= 1;
                        break;
                case "Armor":
                    if(Stats.Armor < Stats.MaxArmor)
                    {
                        Stats.Armor += 1;
                        Stats.SkillPoints -= 1;
                    }
                    break;
                case "Strenght":
                    Stats.Strenght += 1;
                    Stats.StrenghtModifyer = (Stats.Strenght - 10) / 2;
                    Stats.Damage = Stats.StrenghtModifyer;
                    Stats.SkillPoints -= 1;
                    break;
            }
        }

        public static int DamageRoll()
        {
            int r = Dice.D8() + Dice.D8();
            return r;
        }
    }
}
