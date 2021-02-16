using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetSpell;

namespace Tweetcabulary.Models
{
    public class TwitterUser
    {
        //Private variables
        private bool valid = true;

        public string userHandle { get; }
        private List<Tweet> tweets;

        public List<string> allWords { get; private set; } = new List<string>();
        public int totalWords { get; private set; } = 0;
        public int totalCharacters { get; private set; } = 0;
        public int totalSyllables { get; private set; } = 0;
        public int totalHashtags { get; private set; } = 0;
        public string longestWord { get; private set; } = "";

        //Constructors
        public TwitterUser(bool isValid, string user)
        {
            this.valid = isValid;
            this.userHandle = user;
        }

        public TwitterUser(List<Tweet> tweetList, string user)
        {
            this.userHandle = user;
            if (tweetList.Count == 0)
            {
                this.valid = false;
                this.tweets = new List<Tweet>();
            }
            else
            {
                this.tweets = tweetList;
                CalculateMetrics();
            }

            if(this.totalWords == 0)
            {
                this.valid = false;
            }
        }

        //Private Methods
        /// <summary>
        /// Calculate all retrievable class variables upon initialization of TwitterUser object
        /// As of now these values are not expected to change since tweets are retrieved only once
        /// </summary>
        private void CalculateMetrics()
        {
            foreach (var Tweet in tweets)
            {
                allWords.AddRange(Tweet.words);
                totalWords += Tweet.words.Count();
                totalSyllables += Tweet.GetSyllableCount();
                totalHashtags += Tweet.hashtags.Count();
                totalCharacters += Tweet.GetCharacterCount();
                if (Tweet.longestWord.Length > this.longestWord.Length) this.longestWord = Tweet.longestWord;
            }
        }

        //Public Methods
        public List<string> GetAllTweetText()
        {
            List<string> allTweetText = new List<string>();
            foreach(var tweet in tweets)
            {
                allTweetText.Add(tweet.text);
            }
            return allTweetText;
        }

        public bool IsValidUser()
        {
            return valid;
        }

        public int GetTweetCount()
        {
            return tweets.Count();
        }

    }
}
