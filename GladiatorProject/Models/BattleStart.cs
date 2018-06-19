using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class BattleStart
    {
        [Key]
        public int Id { get; set; }

        public Gladiator Gladiator { get; set; }

        public Opponent Opponent { get; set; }

        //public List<Gladiator> Gladiators { get; set; }

        public List<Opponent> Opponents { get; set; }

    }
}