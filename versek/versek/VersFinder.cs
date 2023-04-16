using System;
using System.Collections.Generic;
using System.Linq;
using versek.Resources;
using Xamarin.Forms.Internals;

namespace versek
{
    public class VersFinder
    {
        public string VersReszlet { get; set; }

        public bool WithoutVowels { get; set; }

        private readonly List<KeyValuePair<char, char>> Vowels = new List<KeyValuePair<char, char>>()
        {
            new KeyValuePair<char, char>('á', 'a'),
            new KeyValuePair<char, char>('é', 'e'),
            new KeyValuePair<char, char>('ó', 'o'),
            new KeyValuePair<char, char>('ö', 'o'),
            new KeyValuePair<char, char>('ő', 'o'),
            new KeyValuePair<char, char>('ú', 'u'),
            new KeyValuePair<char, char>('ü', 'u'),
            new KeyValuePair<char, char>('ű', 'u')
        };

        public List<Vers> Execute()
        {
            if (string.IsNullOrWhiteSpace(VersReszlet.Trim()))
            {
                return new List<Vers>();
            }

            var count = new List<KeyValuePair<Vers, int>>();
            var wordsOfReszlet = GetWordArrayFromString(VersReszlet);

            if (WithoutVowels)
            {
                wordsOfReszlet = GetWordsWithoutVowelsFromStringArray(wordsOfReszlet);
            }

            var tmp = wordsOfReszlet;

            foreach (var vers in Versek.VersLista)
            {
                var wordsOfVersSzoveg = GetWordArrayFromString(vers.VersSzoveg);
                if (WithoutVowels)
                {
                    wordsOfVersSzoveg = GetWordsWithoutVowelsFromStringArray(wordsOfVersSzoveg);
                }
                tmp = wordsOfVersSzoveg; 
                var matches = CountMatchesInVersSzoveg(wordsOfReszlet, wordsOfVersSzoveg);

                var versAndCount = new KeyValuePair<Vers, int> (vers, matches);

                count.Add(versAndCount);
            }

            var mostCountValue = count.Select(kv => kv.Value).Max();
            var versekWithMostCounts = count.Where(kv => kv.Value == mostCountValue).Select(kv => kv.Key).ToList();
            return versekWithMostCounts;
        }

        private int CountMatchesInVersSzoveg(List<string> reszletek, List<string> versSzoveg)
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

        private List<string> GetWordArrayFromString(string szoveg) => szoveg.Trim().Replace(".", string.Empty).Replace(",", string.Empty).Replace($"{Environment.NewLine}", string.Empty).Split(' ').Select(w => w.ToLower()).ToList();

        private List<string> GetWordsWithoutVowelsFromStringArray(List<string> listOfString)
        {
            var newList = new List<string>();

            foreach (var word in listOfString)
            {
                foreach (var vowel in Vowels)
                {
                    newList.Add(word.Replace(vowel.Key, vowel.Value));
                }
            }

            return newList;
        }
    }
}
