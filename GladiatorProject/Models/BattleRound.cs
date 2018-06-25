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
                    if(GladiatorStrike == 20 && OpponentStrike == 20)
                    {
                        var WorldEnd = ($"WORLD END STRIKE both {fighter.Gladiator.Name} and {fighter.Opponent.Name} rolled {GladiatorStrike} on the D20." +
                            $" Making both fighters take double damage.");
                        OHealth -= fighter.Gladiator.Damage * 2;
                        GHealth -= fighter.Opponent.Damage * 2;
                        var BothCritDamageDealt = ($"{fighter.Gladiator.Name} dealt {fighter.Gladiator.Damage * 2} damage to {fighter.Opponent.Name}" +
                           $" and {fighter.Opponent.Name} dealt {fighter.Opponent.Damage * 2} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(WorldEnd.ToString());
                        Rounds.Add(BothCritDamageDealt.ToString());
                    }
                    else
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

                }
                else if (GladiatorStrike >= fighter.Opponent.Armor && OpponentStrike < fighter.Gladiator.Armor)
                {
                    if(GladiatorStrike == 20)
                    {
                        var GladiatorCrit = ($"CRITICAL HIT by {fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                        $" {fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                        $" {fighter.Gladiator.Name} critical hits and {fighter.Opponent.Name} misses.");
                        OHealth -= fighter.Gladiator.Damage * 2;
                        var GladiatorCritDamageDealt = ($"{fighter.Gladiator.Name} dealt critical {fighter.Gladiator.Damage * 2} damage to {fighter.Opponent.Name}.");
                        Rounds.Add(GladiatorCrit.ToString());
                        Rounds.Add(GladiatorCritDamageDealt.ToString());
                    }
                    else
                    {
                        var GladiatorHit = ($"{fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                        $" {fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                        $" {fighter.Gladiator.Name} hits and {fighter.Opponent.Name} misses.");
                        OHealth -= fighter.Gladiator.Damage;
                        var GladiatorDamageDealt = ($"{fighter.Gladiator.Name} dealt {fighter.Gladiator.Damage} damage to {fighter.Opponent.Name}.");
                        Rounds.Add(GladiatorHit.ToString());
                        Rounds.Add(GladiatorDamageDealt.ToString());
                    }
                    
                }
                else if (OpponentStrike >= fighter.Gladiator.Armor && GladiatorStrike < fighter.Opponent.Armor)
                {
                    if(OpponentStrike == 20)
                    {
                        var OpponentCritHit = ($"CRITICAL HIT by {fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                       $" {fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                       $" {fighter.Opponent.Name} critical hits and {fighter.Gladiator.Name} misses.");
                        GHealth -= fighter.Opponent.Damage * 2;
                        var OpponentCritDamageDealt = ($"{fighter.Opponent.Name} dealt critical {fighter.Opponent.Damage * 2} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(OpponentCritHit.ToString());
                        Rounds.Add(OpponentCritDamageDealt.ToString());
                    }
                    else
                    {
                        var OpponentHit = ($"{fighter.Opponent.Name} rolled a {OpponentStrike} on the D20 against {fighter.Gladiator.Name} {fighter.Gladiator.Armor} armor." +
                       $" {fighter.Gladiator.Name} rolled a {GladiatorStrike} on the D20 against {fighter.Opponent.Name} {fighter.Opponent.Armor} armor." +
                       $" {fighter.Opponent.Name} hits and {fighter.Gladiator.Name} misses.");
                        GHealth -= fighter.Opponent.Damage;
                        var OpponentDamageDealt = ($"{fighter.Opponent.Name} dealt {fighter.Opponent.Damage} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(OpponentHit.ToString());
                        Rounds.Add(OpponentDamageDealt.ToString());
                    }
                   
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
            if (GHealth <= 0 & OHealth <= 0)
            {
                var Draw = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both succumbed to wounds making it a draw.");
                fighter.Gladiator.Health = 0;
                fighter.Opponent.Health = 0;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesDraw += 1;
                Rounds.Add(Draw.ToString());
            }
            else if (OHealth <= 0)
            {
                var Win = ($"{fighter.Gladiator.Name} won the battle over {fighter.Opponent.Name}.");
                fighter.Gladiator.Health = GHealth;
                fighter.Opponent.Health = 0;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesWon += 1;
                int ExpGain = 0;
                int GoldGain = 0;
                if (fighter.Gladiator.Level == fighter.Opponent.Level)
                {
                    ExpGain = 10 + Dice.D20();
                    GoldGain = 5 + Dice.D6() + Dice.D6();
                    fighter.Gladiator.Experiance += ExpGain;
                    fighter.Gladiator.Gold += GoldGain;
                }
                if (fighter.Gladiator.Level < fighter.Opponent.Level)
                {
                    ExpGain = 15 + Dice.D20() + Dice.D10();
                    GoldGain = 7 + Dice.D6() + Dice.D6() + Dice.D4();
                    fighter.Gladiator.Experiance += ExpGain;
                    fighter.Gladiator.Gold += GoldGain;
                }
                if (fighter.Gladiator.Level > fighter.Opponent.Level)
                {
                    ExpGain = 5 + Dice.D10();
                    GoldGain = 3 + Dice.D6();
                    fighter.Gladiator.Experiance += ExpGain;
                    fighter.Gladiator.Gold += GoldGain;
                }
                var Earned = ($"{fighter.Gladiator.Name} gained {ExpGain} Experience and {GoldGain} Gold.");
                Rounds.Add(Win.ToString());
                Rounds.Add(Earned.ToString());
                fighter.Gladiator.DefeatedOpponent.Add(fighter.Opponent);
                
            }
            else if (GHealth <= 0)
            {
                var Lost = ($"{fighter.Gladiator.Name} lost the battle to {fighter.Opponent.Name}.");
                fighter.Gladiator.Health = 0;
                fighter.Opponent.Health = OHealth;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesLost += 1;
                Rounds.Add(Lost.ToString());
            }
        }
      
    }
}