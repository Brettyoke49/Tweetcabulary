using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetcabulary.Models
{
    public class Tweet
    {
        //Private variables
        public string text { get; }
        public List<string> words { get; }
        public List<string> hashtags { get; }
        public string longestWord { get; private set; }
        private string userHandle { get; }

        //Constructors

        /// <summary>
        /// Constructor to flesh out Tweet object
        /// </summary>
        /// <param name="t">Tweet text</param>
        /// <param name="h">List of hashtags in a tweet</param>
        /// <param name="u">Username of tweet owner</param>
        public Tweet(string t, List<string> h, string u)
        {
            longestWord = "";
            this.text = t;
            this.hashtags = h;
            this.userHandle = u;
            this.words = new List<string>();

            CreateWordList(t);
        }

        //Private Methods
        /// <summary>
        /// To be called from constructor only
        /// </summary>
        /// <param name="tweetText">Text of the tweet</param>
        private void CreateWordList(string tweetText)
        {
            foreach (string word in tweetText.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                if(word.Length > this.longestWord.Length)
                {
                    this.longestWord = word;
                }
                this.words.Add(word);
            }
        }

        /// <summary>
        /// Count the syllables in a given word from a tweet
        /// </summary>
        /// <param name="word">Single dictionary word</param>
        /// <returns>Approximate syllable count</returns>
        private int CountSyllables(string word)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            string currentWord = word;
            int numVowels = 0;
            bool lastWasVowel = false;
            foreach (char wc in currentWord)
            {
                bool foundVowel = false;
                foreach (char v in vowels)
                {
                    //don't count diphthongs
                    if (v == wc && lastWasVowel)
                    {
                        foundVowel = true;
                        lastWasVowel = true;
                        break;
                    }
                    else if (v == wc && !lastWasVowel)
                    {
                        numVowels++;
                        foundVowel = true;
                        lastWasVowel = true;
                        break;
                    }
                }

                //if full cycle and no vowel found, set lastWasVowel to false;
                if (!foundVowel)
                    lastWasVowel = false;
            }
            //remove es, it's _usually? silent
            if (currentWord.Length > 2 &&
                currentWord.Substring(currentWord.Length - 2) == "es")
                numVowels--;
            // remove silent e
            else if (currentWord.Length > 1 &&
                currentWord.Substring(currentWord.Length - 1) == "e")
                numVowels--;

            return numVowels;
        }


        //Public Methods
        public int GetSyllableCount()
        {
            int SyllableSum = 0;

            foreach (string word in words)
            {
                SyllableSum += CountSyllables(word);
            }

            return SyllableSum;
        }

        public int GetCharacterCount()
        {
            int characterCount = 0;
            foreach (string word in words)
            {
                characterCount += word.Length;
            }

            return characterCount;
        }
    }
}
