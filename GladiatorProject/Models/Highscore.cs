using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Highscore
    {
        [Key]
        public int Id { get; set; }

        //public ApplicationUser PlayerScores { get; set; }
        [ForeignKey("PlayerScores")]
        public string User_Id { get; set; }
        public List<ApplicationUser> PlayerScores { get; set; }

        //public Gladiator GladiatorScores { get; set; }
        [ForeignKey("GladiatorScores")]
        public int Gladiator_Id { get; set; }

        public List<Gladiator> GladiatorScores { get; set; }

        public Highscore()
        {
            PlayerScores = new List<ApplicationUser>(); //constructor to active the Lists for player and gladiator scores.
            GladiatorScores = new List<Gladiator>();
        }
    }
}