using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetcabulary.Models
{
    public class UserAnalysis
    {
        //Private members
        private TwitterUser twitterUser;

        private ITwitAPI twitService;
        private ISpellCheck spellService;

        //Constructor
        public UserAnalysis(string userHandle, ITwitAPI twitService, ISpellCheck spellService)
        {
            //Forcing user to enter @ to make clear what name is needed, but we don't actually want it
            if (userHandle[0] == '@') userHandle.Substring(1);

            this.spellService = spellService;

            this.twitService = twitService;
            var waitTweets = this.twitService.GetUserTweets(userHandle);
            waitTweets.Wait();

            this.twitterUser = waitTweets.Result;

            if(!twitterUser.IsValidUser())
            {

            }
        }

        //Private Methods
        private int CountMisspelledWords(List<string> words)
        {
            int totalMisspelledWords = 0;
            foreach (string word in words)
            {
                bool valid = spellService.IsValidWord(word);
                if (!valid) totalMisspelledWords++;
            }
            return totalMisspelledWords;
        }

        //Public Methods
        public string GetHandle()
        {
            return twitterUser.userHandle;
        }

        public List<string> GetTweets()
        {
            return twitterUser.GetAllTweetText();
        }

        public int GetTweetCount()
        {
            return twitterUser.GetTweetCount();
        }

        //Analytics methods
        public decimal AverageWordLength()
        {
            decimal avgLength = (decimal)twitterUser.totalCharacters / (decimal)twitterUser.totalWords;
            return Math.Round(avgLength, 2, MidpointRounding.AwayFromZero);
        }

        public decimal AverageTweetLength()
        {
            decimal avgTweetLength = (decimal)twitterUser.totalWords / (decimal)twitterUser.GetTweetCount();
            return Math.Round(avgTweetLength, 2, MidpointRounding.AwayFromZero);
        }

        public decimal PercentMisspelledWords()
        {
            int misspelledWords = CountMisspelledWords(twitterUser.allWords);
            decimal percentMisspelled = Math.Round(((decimal)misspelledWords / (decimal)twitterUser.totalWords) * 100, 2, MidpointRounding.AwayFromZero);
            return percentMisspelled;
        }

        public decimal AverageSyllables()
        {
            decimal avgSyllables = (decimal)twitterUser.totalSyllables / (decimal)twitterUser.totalWords;
            return Math.Round(avgSyllables, 2, MidpointRounding.AwayFromZero);
        }

        public decimal AverageHashtags()
        {
            decimal avgHashtags = (decimal)twitterUser.totalHashtags / (decimal)twitterUser.GetTweetCount();
            return Math.Round(avgHashtags, 2, MidpointRounding.AwayFromZero);
        }
        
        public string LongestWord()
        {
            return twitterUser.longestWord;
        }

        /// <summary>
        /// Top 10 words used excluding basic articles and pronouns
        /// </summary>
        /// <returns>List of top 10 words</returns>
        public List<string> TopTenWords()
        {
            return new List<string> { "dog", "cat", "brother", "Angelica" };
        }

    }
}
