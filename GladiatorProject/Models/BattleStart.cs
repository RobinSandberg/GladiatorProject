using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class BattleStart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Gladiator")]
        public int Gladiator_ID { get; set; }
        
        public Gladiator Gladiator { get; set; } // saving the gladiator and oppoenent selected to the class.

        public Opponent Opponent { get; set; }

        public string Finished { get; set; }

        public DateTime BattleDate { get; set; }
    }
}