using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class ClassRole
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Weapon { get; set; }

        public int Damage { get; set; }

        public int Armor { get; set; }

        public int Health { get; set; }

        public int SkillPoints = 1;

        public ClassRole()
        {

        }
    }
}