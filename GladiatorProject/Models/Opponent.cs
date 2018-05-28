using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Opponent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string Class { get; set; }

        public int Health { get; set; }

        public int Speed { get; set; }

        public int Dexterity { get; set; }

        public int Strenght { get; set; }

        public int Constituion { get; set; }

        public int Level { get; set; }
    }
}