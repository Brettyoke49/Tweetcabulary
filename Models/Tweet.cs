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
        public string longestWord { get; set; }
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


        //Public Methods
        public int GetSyllableCount() //TODO
        {

            return 0;
        }
    }
}
