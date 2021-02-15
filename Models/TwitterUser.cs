using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetcabulary.Models
{
    public class TwitterUser
    {
        //Private variables
        private bool valid = true;

        public string userHandle { get; }
        private List<Tweet> tweets;

        //Constructors
        public TwitterUser()
        {

        }

        public TwitterUser(bool isValid, string user)
        {
            this.valid = isValid;
            this.userHandle = user;
        }

        public TwitterUser(List<Tweet> tweetList, string user)
        {
            if (tweetList.Count == 0) 
            {
                this.valid = false;
            }
            this.userHandle = user;
            this.tweets = tweetList;
        }

        //Private Methods

        //Public Methods
        public List<String> GetAllTweetText()
        {
            List<String> allTweetText = new List<string>();
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
