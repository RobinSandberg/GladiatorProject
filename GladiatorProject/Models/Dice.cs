using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Dice
    {
        static Random _roll = new Random(); // Random rolled dice from 1 to 20.

        public static int D20()
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
       


    }
}