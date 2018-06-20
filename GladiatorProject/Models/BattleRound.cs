using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GladiatorProject.Models;

namespace GladiatorProject.Models
{
    public class BattleRound
    {
        [Key]
        public int Id { get; set; }

        public List<string> Rounds = new List<string>();

        public void Round(BattleStart fighter)
        {
            Id = fighter.Id;
            int GHealth = fighter.Gladiator.Health;
            int OHealth = fighter.Opponent.Health; 

            while (GHealth > 0 && OHealth > 0)
            {
                int GladiatorStrike = Dice.D20();
                int OpponentStrike = Dice.D20();

                if (GladiatorStrike >= fighter.Opponent.Armor && OpponentStrike >= fighter.Gladiator.Armor)
                {
                    var BothHit = ($"{fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                        $" {fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                        $" Both hitting dealing damage.");
                    OHealth -= fighter.Gladiator.Damage;
                    GHealth -= fighter.Opponent.Damage;
                    var BothDamageDealt = ($"{fighter.Gladiator.Name} dealt {fighter.Gladiator.Damage} damage to {fighter.Opponent.Name}" +
                        $" and {fighter.Opponent.Name} dealt {fighter.Opponent.Damage} damage to {fighter.Gladiator.Name}.");
                    Rounds.Add(BothHit.ToString());
                    Rounds.Add(BothDamageDealt.ToString());
                   
                  
                }
                else if (GladiatorStrike >= fighter.Opponent.Armor && OpponentStrike < fighter.Gladiator.Armor)
                {
                    var GladiatorHit = ($"{fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                        $" {fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                        $" {fighter.Gladiator.Name} hits and {fighter.Opponent.Name} misses.");
                    OHealth -= fighter.Gladiator.Damage;
                    var GladiatorDamageDealt = ($"{fighter.Gladiator.Name} dealt {fighter.Gladiator.Damage} damage to {fighter.Opponent.Name}.");
                    Rounds.Add(GladiatorHit.ToString());
                    Rounds.Add(GladiatorDamageDealt.ToString());
                }
                else if (OpponentStrike >= fighter.Gladiator.Armor && GladiatorStrike < fighter.Opponent.Armor)
                {
                    var OpponentHit = ($"{fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                        $" {fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                        $" {fighter.Opponent.Name} hits and {fighter.Gladiator.Name} misses.");
                    GHealth -= fighter.Opponent.Damage;
                    var OpponentDamageDealt = ($"{fighter.Opponent.Name} dealt {fighter.Opponent.Damage} damage to {fighter.Gladiator.Name}.");
                    Rounds.Add(OpponentHit.ToString());
                    Rounds.Add(OpponentDamageDealt.ToString());
                }
                else if(GladiatorStrike < fighter.Opponent.Armor && OpponentStrike < fighter.Gladiator.Armor)
                {
                    var Regain = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both swing and miss recovering health.");
                    if(GHealth == fighter.Gladiator.FullHealth)
                    {

                    }
                    else if(GHealth < fighter.Gladiator.FullHealth)
                    {
                        GHealth += 5;

                        if(GHealth > fighter.Gladiator.FullHealth)
                        {
                            GHealth = fighter.Gladiator.FullHealth;
                        }
                    }
                    if (OHealth == fighter.Opponent.FullHealth)
                    {

                    }
                    else if (OHealth < fighter.Opponent.FullHealth)
                    {
                        OHealth += 5;

                        if (OHealth > fighter.Opponent.FullHealth)
                        {
                            OHealth = fighter.Opponent.FullHealth;
                        }
                    }
                    Rounds.Add(Regain.ToString());

                }

                var Status = ($"{fighter.Gladiator.Name} has {GHealth} health left. {fighter.Opponent.Name} has {OHealth} health left.");
                Rounds.Add(Status.ToString());

            }
            if (OHealth <= 0)
            {
                var Win = ($"{fighter.Gladiator.Name} won the battle.");
                fighter.Gladiator.Health = GHealth;
                fighter.Opponent.Health = OHealth;
                if (fighter.Gladiator.Level == fighter.Opponent.Level)
                {
                    fighter.Gladiator.Experiance += 10 + Dice.D20();
                }
                if (fighter.Gladiator.Level < fighter.Opponent.Level)
                {
                    fighter.Gladiator.Experiance += 15 + Dice.D20() + Dice.D10();
                }
                if (fighter.Gladiator.Level > fighter.Opponent.Level)
                {
                    fighter.Gladiator.Experiance += 5 + Dice.D10();
                }
                Rounds.Add(Win.ToString());
                
            }
            if (GHealth <= 0)
            {
                var Lost = ($"{fighter.Opponent.Name} won the battle.");
                fighter.Gladiator.Health = GHealth;
                fighter.Opponent.Health = OHealth;
                Rounds.Add(Lost.ToString());
            }

        }
      
    }
}