using System.Collections.Generic;
using System.Linq;
using versek.Resources;

namespace versek
{
    public class VersFinder
    {
        public string VersReszlet { get; set; }

        public List<Vers> Execute()
        {
            if (string.IsNullOrWhiteSpace(VersReszlet.Trim()))
            {
                return new List<Vers>();
            }

            var count = new List<KeyValuePair<Vers, int>>();
            var wordsOfReszlet = GetWordArrayFromString(VersReszlet);
            foreach (var vers in Versek.VersLista)
            {
                var wordsOfVersSzoveg = GetWordArrayFromString(vers.VersSzoveg);
                var matches = CountMatchesInVersSzoveg(wordsOfReszlet, wordsOfVersSzoveg);

                var versAndCount = new KeyValuePair<Vers, int> (vers, matches);

                count.Add(versAndCount);
            }

            var mostCountValue = count.Select(kv => kv.Value).Max();
            var versekWithMostCounts = count.Where(kv => kv.Value == mostCountValue).Select(kv => kv.Key).ToList();
            return versekWithMostCounts;
        }

        private int CountMatchesInVersSzoveg(string[] reszletek, string[] versSzoveg)
        {
            var matches = 0;
            foreach (var reszlet in reszletek)
            {
                foreach (var szo in versSzoveg)
                {
                    if ((reszlet.Length > 3 && szo.Contains(reszlet)) || reszlet == szo)
                    {
                        matches++;
                        break;
                    }
                }
            }

            return matches;
        }

        private string[] GetWordArrayFromString(string szoveg) => szoveg.Trim().Replace(".", string.Empty).Replace(",", string.Empty).Split(' ').Select(w => w.ToLower()).ToArray();
    }
}
