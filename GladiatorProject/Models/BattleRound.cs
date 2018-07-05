using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GladiatorProject.Models;
using System.Data.Entity;

namespace GladiatorProject.Models
{
    public class BattleRound
    {
        [Key]
        public int Id { get; set; }

        //public List<string> Rounds { get; set; }
        public List<string> Rounds = new List<string>();  // List of the strings printed out for the player to see how the battle went.

        //public BattleRound()
        //{
        //    Rounds = new List<string>();
        //}
        public void Round(BattleStart fighter)
        {
            Id = fighter.Id;   //Adding the same Id to battleround as the battleStart.
            int GHealth = fighter.Gladiator.Health;  // Variable for temporary health during the battle for gladiator.
            int OHealth = fighter.Opponent.Health;

            while (GHealth > 0 && OHealth > 0)   // A loop for the battle while both fighters got over 0 health.
            {
                int GladiatorStrike = Dice.D20();  //A role for gladiator from 1 to 20 to determine the base roll and if it strike crit.
                int Gladiatorattack = GladiatorStrike + fighter.Gladiator.StrenghtModifyer;  // Taking the base role and adding the gladiators modifyer to determine the attack value.
                int OpponentStrike = Dice.D20();
                int Opponentattack = OpponentStrike + fighter.Opponent.StrenghtModifyer;
                int GladiatorDamage = fighter.Gladiator.StrenghtModifyer + Gladiator.DamageRoll(); //Rolling the damage per strike for gladiator with strenght modifyer and the Damage dice set.
                int OpponentDamage = fighter.Opponent.StrenghtModifyer + Gladiator.DamageRoll(); // using same damageroll as gladiator to save functions.
                if (Gladiatorattack >= fighter.Opponent.Armor && Opponentattack >= fighter.Gladiator.Armor) // Both fighters hit eachothere.
                {
                    if(GladiatorStrike == 20 && OpponentStrike == 20) // Both fighters crit eachother.
                    {
                        var WorldEnd = ($"WORLD END STRIKE both {fighter.Gladiator.Name} and {fighter.Opponent.Name} both find a opening for a strike to the head." +
                            $" Making both fighters take double damage.");  // string saved on what type of strike and who hit who.
                        OHealth -= GladiatorDamage * 2; // damage roll times 2.
                        GHealth -= OpponentDamage * 2;
                        var BothCritDamageDealt = ($"{fighter.Gladiator.Name} dealt {GladiatorDamage * 2} damage to {fighter.Opponent.Name}" +
                           $" and {fighter.Opponent.Name} dealt {OpponentDamage * 2} damage to {fighter.Gladiator.Name}."); // string on the damage dealt.
                        Rounds.Add(WorldEnd.ToString());
                        Rounds.Add(BothCritDamageDealt.ToString());
                        if (OHealth <= 0 && GHealth <= 0) // If both fighters die from the critical hit.
                        {
                            var Headless = ($"Both fighters died from double decapetation.");
                            Rounds.Add(Headless.ToString());
                        } 
                    }
                    else  // Hit without critical.
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
                else if (Gladiatorattack >= fighter.Opponent.Armor && Opponentattack < fighter.Gladiator.Armor) // Gladiator hit and opponent miss
                {
                    if(GladiatorStrike == 20) // Gladiator critical hit
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
                    else // Regular hit
                    {
                        var GladiatorHit = ($"{fighter.Gladiator.Name} deflected the strike from {fighter.Opponent.Name} before striking back against the body.");
                        OHealth -= GladiatorDamage;
                        var GladiatorDamageDealt = ($"{fighter.Gladiator.Name} dealt {GladiatorDamage} damage to {fighter.Opponent.Name}.");
                        Rounds.Add(GladiatorHit.ToString());
                        Rounds.Add(GladiatorDamageDealt.ToString());
                    }
                    
                }
                else if (Opponentattack >= fighter.Gladiator.Armor && Gladiatorattack < fighter.Opponent.Armor) // Opponent hit and gladiator miss.
                {
                    if(OpponentStrike == 20) // opponent critical hit.
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
                    else // opponent regular hit
                    {
                        var OpponentHit = ($"{fighter.Opponent.Name} deflected the strike from {fighter.Gladiator.Name} before striking back against the body.");
                        GHealth -= OpponentDamage;
                        var OpponentDamageDealt = ($"{fighter.Opponent.Name} dealt {OpponentDamage} damage to {fighter.Gladiator.Name}.");
                        Rounds.Add(OpponentHit.ToString());
                        Rounds.Add(OpponentDamageDealt.ToString());
                    }
                   
                }
                else if(Gladiatorattack < fighter.Opponent.Armor && Opponentattack < fighter.Gladiator.Armor) //Both fighters miss eachothere.
                {
                    var Regain = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both swing and miss recovering health in the short recovering period.");
                    if(GHealth == fighter.Gladiator.FullHealth) // Checking if health is full
                    {

                    }
                    else if(GHealth < fighter.Gladiator.FullHealth) // Health not full you gain 5 health back
                    {
                        GHealth += 5;

                        if(GHealth > fighter.Gladiator.FullHealth)  // if the +5 health goes over full health the health is set to full.
                        {
                            GHealth = fighter.Gladiator.FullHealth;
                        }
                    }
                    if (OHealth == fighter.Opponent.FullHealth) // Opponent same as gladiator health regen.
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
                if(OHealth >= 1 && GHealth >= 1) // As long as both fighter got 1 health or more the status string will be added.
                {
                    var Status = ($"{fighter.Gladiator.Name} has {GHealth} health left. {fighter.Opponent.Name} has {OHealth} health left.");
                    Rounds.Add(Status.ToString());
                }

            }
            if (GHealth <= 0 && OHealth <= 0)   // Both players got 0 or less health making the fight draw.
            {
                var Draw = ($"{fighter.Gladiator.Name} and {fighter.Opponent.Name} both succumbed to wounds making it a draw.");
                fighter.Gladiator.Health = 0; // setting the health to 0 incase it go below 0 to help reduce the cost of healing back up agian.
                fighter.Gladiator.Battles += 1;  // add 1 battle to the gladiator.
                fighter.Gladiator.BattlesDraw += 1;// add 1 draw to the gladiator.
                fighter.Gladiator.TempLost = 1;// add a temporary point to keep track of if you last fight was a draw or lost.
                if (fighter.Gladiator.CurrentWinningStreak > fighter.Gladiator.BestWinningStreak) // checking the win streak if the current is higher then the best it will be saved as the new best.
                {                                                                                // before resting the streak to 0.
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
            else if (OHealth <= 0) // Opponent reach 0 or less health gladiator wins.
            {
                var Win = ($"{fighter.Gladiator.Name} won the battle over {fighter.Opponent.Name}.");
                fighter.Gladiator.Health = GHealth; // setting the temporary health to the new health.
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesWon += 1;
                fighter.Gladiator.CurrentWinningStreak += 1; // adding a win streak to the gladiator.
                fighter.Gladiator.BestWinningStreak = fighter.Gladiator.CurrentWinningStreak;
                int ExpGain = 0;  //variables for exp gold and score gain.
                int GoldGain = 0;
                int ScoreGain = 0;
                if (fighter.Gladiator.Level == fighter.Opponent.Level)  // fighted same level opponent as gladiator level.
                {
                    ExpGain = 10 + Dice.D20();  // gain 10 exp and random 1 to 20 added.
                    GoldGain = 5 + Dice.D6() + Dice.D6(); // the gold earned 5 and then 2 random D6s to add 2 to 12 gold.
                    ScoreGain = 4 + Dice.D6();  // score 4 base with 1 to 6 added.
                    fighter.Gladiator.GladiatorScore += ScoreGain;
                    if(fighter.Gladiator.GladiatorScore > fighter.Gladiator.GladiatorHighScore)
                    {
                        fighter.Gladiator.GladiatorHighScore = fighter.Gladiator.GladiatorScore;
                    }
                    fighter.Gladiator.Experiance += ExpGain;
                    fighter.Gladiator.Gold += GoldGain;
                }
                if (fighter.Gladiator.Level < fighter.Opponent.Level)  // if the opponent higher lvl then gladiator a small increase in gain.
                {
                    ExpGain = 15 + Dice.D20() + Dice.D10();
                    GoldGain = 7 + Dice.D6() + Dice.D6() + Dice.D4();
                    ScoreGain = 6 + Dice.D8();
                    fighter.Gladiator.GladiatorScore += ScoreGain;
                    if (fighter.Gladiator.GladiatorScore > fighter.Gladiator.GladiatorHighScore)
                    {
                        fighter.Gladiator.GladiatorHighScore = fighter.Gladiator.GladiatorScore;
                    }
                    fighter.Gladiator.Experiance += ExpGain;
                    fighter.Gladiator.Gold += GoldGain;
                }
                if (fighter.Gladiator.Level > fighter.Opponent.Level) // if the opponent lower lvl then gladiator a small decrease in gain.
                {
                    ExpGain = 5 + Dice.D10();
                    GoldGain = 3 + Dice.D6();
                    ScoreGain = 2 + Dice.D4();
                    fighter.Gladiator.GladiatorScore += ScoreGain;
                    if (fighter.Gladiator.GladiatorScore > fighter.Gladiator.GladiatorHighScore)
                    {
                        fighter.Gladiator.GladiatorHighScore = fighter.Gladiator.GladiatorScore;
                    }
                    fighter.Gladiator.Experiance += ExpGain;
                    fighter.Gladiator.Gold += GoldGain;
                }
                var Earned = ($"{fighter.Gladiator.Name} gained {ExpGain} Experience , {GoldGain} Gold and {ScoreGain} Score.");
                Rounds.Add(Win.ToString());
                Rounds.Add(Earned.ToString());
                fighter.Gladiator.LastBattle = Win.ToString();

            }
            else if (GHealth <= 0) // if gladiator health 0 of lower.
            {
                var Lost = ($"{fighter.Gladiator.Name} lost the battle to {fighter.Opponent.Name}.");

                fighter.Gladiator.Health = 0;
                fighter.Gladiator.Battles += 1;
                fighter.Gladiator.BattlesLost += 1;
                fighter.Gladiator.TempLost = 1;
                fighter.Gladiator.GladiatorScore = 0; // setting the score count to 0 again.  only reset on lost atm not draw.
                if (fighter.Gladiator.CurrentWinningStreak > fighter.Gladiator.BestWinningStreak)  // checking the winstreak and save if needed then reset to 0 agian.
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