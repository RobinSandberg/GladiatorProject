using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Dice
    {
        static Random _roll = new Random();
        // The dices for the game in the diffrent sizes.
        public static int D20()  // Random rolled dice from 1 to 20.
        {
            int n = _roll.Next(1, 21);
            return n;
        }
        public static int D12()
        {
            int n = _roll.Next(1, 13);
            return n;
        }

        public static int D10()
        {
            int n = _roll.Next(1, 11);
            return n;
        }
        public static int D8()
        {
            int n = _roll.Next(1, 9);
            return n;
        }
        public static int D6()
        {
            int n = _roll.Next(1, 7);
            return n;
        }
        public static int D4()
        {
            int n = _roll.Next(1, 5);
            return n;
        }

        public static int D5()
        {
            int n = _roll.Next(1, 6);
            return n;
        }

    }
}