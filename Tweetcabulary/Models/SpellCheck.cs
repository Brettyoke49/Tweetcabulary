using System;
using System.Collections.Generic;
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
        private ASPNetSpell.SpellChecker spellChecker;

        //Constructor
        public SpellCheck()
        {
            //Spellchecker from Spell.Check library
            spellChecker = new ASPNetSpell.SpellChecker();
        }

        //Public Methods

        //Call at startup to force initialization and authentication of this singleton
        public void loadWords(string path) 
        {
            spellChecker.LoadCustomDictionary(path);
            var liveDictionaries = spellChecker.ListLiveDictionaries();
            if(liveDictionaries.Count != 1)
            {
                throw new Exception("Failed to load dictionary from path: " + path);
            }
        }

        public bool IsValidWord(string word)
        {
            if (String.IsNullOrEmpty(word)) return false;
            return spellChecker.SimpleSpell(word, true);
        }
    }
}
