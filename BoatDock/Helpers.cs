using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatDock
{
    public class Helpers
    {
        public static string GetThreeRandomLetters()
        {
            Random rnd = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, 3).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

    }
}
