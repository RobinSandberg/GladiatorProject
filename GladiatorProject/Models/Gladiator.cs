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

        public int Battles { get; set; }

        public int BattlesWon { get; set; }

        public int BattlesLost { get; set; }

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
            Battles = 0;
            BattlesWon = 0;
            BattlesLost = 0;
        }

    }
}
