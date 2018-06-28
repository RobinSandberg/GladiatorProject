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

        public string DamageDice { get; set; }

        public int Strenght { get; set; }

        public int StrenghtModifyer { get; set; }

        public int Constitution { get; set; }

        public int ConstitutionModifyer { get; set; }

        public int Level { get; set; }

        /*public List<int> Levels = new List<int>();*/  // Used when setting level range against gladiator.
        public List<Opponent> Levels = new List<Opponent>();

        //public Opponent()
        //{
        //    Armor = 5 + Dice.D6() + Dice.D6();
        //    Constitution = 5 + Dice.D6() + Dice.D6();
        //    ConstitutionModifyer = (Constitution - 10) / 2;
        //    Strenght = 5 + Dice.D6() + Dice.D6();
        //    StrenghtModifyer = (Strenght - 10) / 2;
        //    FullHealth = Dice.D10() + ConstitutionModifyer;
        //    Health = FullHealth;
        //    Damage = StrenghtModifyer;
        //    DamageDice = "2xD8";

        //}

        public static void EnemyStats(Opponent enemy)
        //public Opponent(Opponent enemy)
        {
            //InfoGenerator names = new InfoGenerator(DateTime.Now.Millisecond);
            //Gender gender = Gender.Any;
            //enemy.Name = names.NextFullName(gender);
            switch (enemy.Level)
            {
                case 1:
                    enemy.Armor = 8 + Dice.D6();
                    enemy.Constitution = 5 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 5 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = Dice.D10() + enemy.ConstitutionModifyer;
                    enemy.Health = enemy.FullHealth;
                    enemy.Damage = enemy.StrenghtModifyer;
                    enemy.DamageDice = "2xD8";
                    break;
                case 2:
                    enemy.Armor = 8 + Dice.D6();
                    enemy.Constitution = 6 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 6 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = Dice.D10() + Dice.D6() + enemy.ConstitutionModifyer;
                    enemy.Health = enemy.FullHealth;
                    enemy.Damage = enemy.StrenghtModifyer;
                    enemy.DamageDice = "2xD8";
                    break;
                case 3:
                    enemy.Armor = 8 + Dice.D6();
                    enemy.Constitution = 7 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 7 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = Dice.D10() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer;
                    enemy.Health = enemy.FullHealth;
                    enemy.Damage = enemy.StrenghtModifyer;
                    enemy.DamageDice = "2xD8";
                    break;
                case 4:
                    enemy.Armor = 9 + Dice.D6();
                    enemy.Constitution = 8 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 8 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer;
                    enemy.Health = enemy.FullHealth;
                    enemy.Damage = enemy.StrenghtModifyer;
                    enemy.DamageDice = "2xD8";
                    break;
                case 5:
                    enemy.Armor = 9 + Dice.D6();
                    enemy.Constitution = 9 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 9 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer;
                    enemy.Health = enemy.FullHealth;
                    enemy.Damage = enemy.StrenghtModifyer;
                    enemy.DamageDice = "2xD8";
                    break;
                case 6:
                    enemy.Armor = 9 + Dice.D6();
                    enemy.Constitution = 10 + Dice.D6() + Dice.D6();
                    enemy.ConstitutionModifyer = (enemy.Constitution - 10) / 2;
                    enemy.Strenght = 10 + Dice.D6() + Dice.D6();
                    enemy.StrenghtModifyer = (enemy.Strenght - 10) / 2;
                    enemy.FullHealth = Dice.D10() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + Dice.D6() + enemy.ConstitutionModifyer;
                    enemy.Health = enemy.FullHealth;
                    enemy.Damage = enemy.StrenghtModifyer;
                    enemy.DamageDice = "2xD8";
                    break;
                case 7:
                    enemy.Health = 16 + Dice.D12();
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Damage = 7/* + Dice.D6()*/;
                    break;
                case 8:
                    enemy.Health = 17 + Dice.D12();
                    enemy.Armor = 10 + Dice.D6();
                    enemy.Damage = 8/* + Dice.D6()*/;
                    break;
                case 9:
                    enemy.Health = 18 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 9 + Dice.D6();
                    break;
                case 10:
                    enemy.Health = 19 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 10 + Dice.D6();
                    break;
                case 11:
                    enemy.Health = 20 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 11 + Dice.D6();
                    break;
                case 12:
                    enemy.Health = 21 + Dice.D12();
                    enemy.Armor = 11 + Dice.D6();
                    enemy.Damage = 12 + Dice.D6();
                    break;
                case 13:
                    enemy.Health = 22 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 13 + Dice.D6();
                    break;
                case 14:
                    enemy.Health = 23 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 14 + Dice.D6();
                    break;
                case 15:
                    enemy.Health = 24 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 15/* + Dice.D6()*/;
                    break;
                case 16:
                    enemy.Health = 25 + Dice.D12();
                    enemy.Armor = 12 + Dice.D6();
                    enemy.Damage = 16/* + Dice.D6()*/;
                    break;
                case 17:
                    enemy.Health = 26 + Dice.D12();
                    enemy.Armor = 14 + Dice.D4();
                    enemy.Damage = 17/* + Dice.D6()*/;
                    break;
                case 18:
                    enemy.Health = 27 + Dice.D12();
                    enemy.Armor = 14 + Dice.D4();
                    enemy.Damage = 18 + Dice.D6();
                    break;
                case 19:
                    enemy.Health = 28 + Dice.D12();
                    enemy.Armor = 14 + Dice.D4();
                    enemy.Damage = 19 + Dice.D6();
                    break;
                case 20:
                    enemy.Health = 29 + Dice.D12();
                    enemy.Armor = 15 + Dice.D4();
                    enemy.Damage = 20 + Dice.D6();
                    break;
                    
            }
            enemy.FullHealth = enemy.Health;
        }
    }
}