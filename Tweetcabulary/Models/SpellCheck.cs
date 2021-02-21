using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetcabulary.Models
{
    public interface ISpellCheck
    {
        void loadWords(string path);
        bool IsValidWord(string word);
    }

    public class SpellCheck : ISpellCheck
    {
        //Private members
        HashSet<string> dictionary;

        //Constructor
        public SpellCheck()
        {
            dictionary = new HashSet<string>();
        }

        //Public Methods

        //Call at startup to load dictionary. Only needed one time per server instance.
        public void loadWords(string path) 
        {
            var lines = File.ReadLines(path);
            foreach(string line in lines)
            {
                dictionary.Add(line);
            }
        }

        public bool IsValidWord(string word)
        {
            if (String.IsNullOrEmpty(word)) return false;
            return dictionary.Contains(word.ToLower());
        }
    }
}
