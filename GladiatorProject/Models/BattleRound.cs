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
                int Gladiatorattack = GladiatorStrike + fighter.Gladiator.StrenghtModifyer;
                int OpponentStrike = Dice.D20();
                int Opponentattack = OpponentStrike + fighter.Opponent.StrenghtModifyer;
                int GladiatorDamage = fighter.Gladiator.Damage + Gladiator.DamageRoll();  //playing around with random damage each time
                int OpponentDamage = fighter.Opponent.Damage + Gladiator.DamageRoll();
                if (Gladiatorattack >= fighter.Opponent.Armor && Opponentattack >= fighter.Gladiator.Armor)
                {
                    if(GladiatorStrike == 20 && OpponentStrike == 20)
                    {
                        var WorldEnd = ($"WORLD END STRIKE both {fighter.Gladiator.Name} and {fighter.Opponent.Name} both find a opening for a strike to the head." +
                            $" Making both fighters take double damage.");
                        OHealth -= GladiatorDamage * 2;
                        GHealth -= OpponentDamage * 2;
                        var BothCritDamageDealt = ($"{fighter.Gladiator.Name} dealt {GladiatorDamage * 2} damage to {fighter.Opponent.Name}" +
                           $" and {fighter.Opponent.Name} dealt {OpponentDamage * 2} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(WorldEnd.ToString());
                        Rounds.Add(BothCritDamageDealt.ToString());
                        if (OHealth <= 0 && GHealth <= 0)
                        {
                            var Headless = ($"Both fighters died from double decapetation.");
                            Rounds.Add(Headless.ToString());
                        } 
                    }
                    else
                    {
                        var BothHit = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both stike past the opponents defenses striking the body." +
                        $" Both hitting dealing damage to eachothere.");
                        OHealth -= GladiatorDamage;
                        GHealth -= OpponentDamage;
                        var BothDamageDealt = ($"{fighter.Gladiator.Name} dealt {GladiatorDamage} damage to {fighter.Opponent.Name}" +
                            $" and {fighter.Opponent.Name} dealt {OpponentDamage} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(BothHit.ToString());
                        Rounds.Add(BothDamageDealt.ToString());
                    }

                }
                else if (Gladiatorattack >= fighter.Opponent.Armor && Opponentattack < fighter.Gladiator.Armor)
                {
                    if(GladiatorStrike == 20)
                    {
                        var GladiatorCrit = ($"CRITICAL HIT by {fighter.Gladiator.Name} was able to dodge  {fighter.Opponent.Name} strike and counter with a strike to the head.");
                        OHealth -= GladiatorDamage * 2;
                        var GladiatorCritDamageDealt = ($"{fighter.Gladiator.Name} dealt critical {GladiatorDamage * 2} damage to {fighter.Opponent.Name}.");
                        Rounds.Add(GladiatorCrit.ToString());
                        Rounds.Add(GladiatorCritDamageDealt.ToString());
                        if (OHealth <= 0)
                        {
                            var Headless = ($"{fighter.Opponent.Name} died from decapetation.");
                            Rounds.Add(Headless.ToString());
                        }  
                    }
                    else
                    {
                        var GladiatorHit = ($"{fighter.Gladiator.Name} deflected the strike from {fighter.Opponent.Name} before striking back against the body.");
                        OHealth -= GladiatorDamage;
                        var GladiatorDamageDealt = ($"{fighter.Gladiator.Name} dealt {GladiatorDamage} damage to {fighter.Opponent.Name}.");
                        Rounds.Add(GladiatorHit.ToString());
                        Rounds.Add(GladiatorDamageDealt.ToString());
                    }
                    
                }
                else if (Opponentattack >= fighter.Gladiator.Armor && Gladiatorattack < fighter.Opponent.Armor)
                {
                    if(OpponentStrike == 20)
                    {
                        var OpponentCritHit = ($"CRITICAL HIT by {fighter.Opponent.Name} was able to dodge {fighter.Gladiator.Name} strike and counter with a strike to the head.");
                        GHealth -= OpponentDamage * 2;
                        var OpponentCritDamageDealt = ($"{fighter.Opponent.Name} dealt critical {OpponentDamage * 2} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(OpponentCritHit.ToString());
                        Rounds.Add(OpponentCritDamageDealt.ToString());
                        if (GHealth <= 0)
                        {
                            var Headless = ($"{fighter.Gladiator.Name} died from decapetation.");
                            Rounds.Add(Headless.ToString());
                        }
                    }
                    else
                    {
                        var OpponentHit = ($"{fighter.Opponent.Name} deflected the strike from {fighter.Gladiator.Name} before striking back against the body.");
                        GHealth -= OpponentDamage;
                        var OpponentDamageDealt = ($"{fighter.Opponent.Name} dealt {OpponentDamage} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(OpponentHit.ToString());
                        Rounds.Add(OpponentDamageDealt.ToString());
                    }
                   
                }
                else if(Gladiatorattack < fighter.Opponent.Armor && Opponentattack < fighter.Gladiator.Armor)
                {
                    var Regain = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both swing and miss recovering health in the short recovering period.");
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
                if(OHealth >= 1 && GHealth >= 1)
                {
                    var Status = ($"{fighter.Gladiator.Name} has {GHealth} health left. {fighter.Opponent.Name} has {OHealth} health left.");
                    Rounds.Add(Status.ToString());
                }

            }
            if (GHealth <= 0 & OHealth <= 0)
            {
                var Draw = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both succumbed to wounds making it a draw.");
                fighter.Gladiator.Health = 0;
                //fighter.Opponent.Health = 0;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesDraw += 1;
                if(fighter.Gladiator.CurrentWinningStreak > fighter.Gladiator.BestWinningStreak)
                {
                    fighter.Gladiator.BestWinningStreak = fighter.Gladiator.CurrentWinningStreak;
                    fighter.Gladiator.CurrentWinningStreak = 0;
                }
                else
                {
                    fighter.Gladiator.CurrentWinningStreak = 0;
                }
                Rounds.Add(Draw.ToString());
                fighter.Gladiator.LastBattle = Draw.ToString();
            }
            else if (OHealth <= 0)
            {
                var Win = ($"{fighter.Gladiator.Name} won the battle over {fighter.Opponent.Name}.");
                fighter.Gladiator.Health = GHealth;
                //fighter.Opponent.Health = 0;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesWon += 1;
                fighter.Gladiator.CurrentWinningStreak += 1;
                fighter.Gladiator.BestWinningStreak = fighter.Gladiator.CurrentWinningStreak;
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
                fighter.Gladiator.LastBattle = Win.ToString();

            }
            else if (GHealth <= 0)
            {
                var Lost = ($"{fighter.Gladiator.Name} lost the battle to {fighter.Opponent.Name}.");

                fighter.Gladiator.Health = 0;
                //fighter.Opponent.Health = OHealth;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesLost += 1;
                if (fighter.Gladiator.CurrentWinningStreak > fighter.Gladiator.BestWinningStreak)
                {
                    fighter.Gladiator.BestWinningStreak = fighter.Gladiator.CurrentWinningStreak;
                    fighter.Gladiator.CurrentWinningStreak = 0;
                }
                else
                {
                    fighter.Gladiator.CurrentWinningStreak = 0;
                }
                Rounds.Add(Lost.ToString());
                fighter.Gladiator.LastBattle = Lost.ToString();
            }
        }

    }
}