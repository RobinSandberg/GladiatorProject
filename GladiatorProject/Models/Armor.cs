using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Armor
    {

        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public int Defense { get; set; }

        public int Dexterity { get; set; }

        public int Strenght { get; set; }

        public int Constituion { get; set; }
    }
}