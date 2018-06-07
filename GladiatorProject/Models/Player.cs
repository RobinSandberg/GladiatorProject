using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        //[Required]
        //public string Class { get; set; }

        public int Health = 10 + Dice.D12();

        public int Armor = 2 + Dice.D8();

        public int Damage = 1 + Dice.D6();

        public int SkillPoints = 0;

        public int Experiance = 0;

        public int Level = 1;

        //[Required]
        //public ClassRole Class { get; set; }

    //    public Fighter Gladiatorclass { get; set; }


    }
    //public enum Fighter
    //{
    //    Murmillo,
    //    Retiarius,
    //    Dimachaerus,
    //    Cestus
    //}
}