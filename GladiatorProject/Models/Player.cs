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

        //public int Health { get; set; }

        //public int Armor { get; set; }

        //public int Damage { get; set; }

        //public int SkillPoints = 5;

        public int Experiance { get; set; }

        public int Level { get; set; }

        //[Required]
        public ClassRole Class { get; set; }

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