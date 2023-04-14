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

            foreach (var vers in Versek.VersLista)
            {
                var wordsOfVersSzoveg = GetWordArrayFromString(vers.VersSzoveg);
                if (WithoutVowels)
                {
                    wordsOfVersSzoveg = GetWordsWithoutVowelsFromStringArray(wordsOfVersSzoveg);
                }
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

        private string[] GetWordsWithoutVowelsFromStringArray(string[] gotArray)
        {
            var length = gotArray.Length;
            var newArray = new string[length];

            gotArray.CopyTo(newArray, length);

            foreach (var word in newArray)
            {
                foreach (var vowel in Vowels)
                {
                    word.Replace(vowel.Key, vowel.Value);
                }
            }

            return newArray;
        }
    }
}
