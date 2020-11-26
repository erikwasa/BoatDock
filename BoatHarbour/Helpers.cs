using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatHarbour
{
    public class Helpers
    {
        public static string GetThreeRandomLetters()
        {
            Random rnd = new Random();
            string AllCapitalChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(AllCapitalChars, 3).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

    }
}
