using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetcabulary.Models
{
    public class UserAnalysis
    {
        //Private members
        private string userHandle;
        private TwitterUser twitterUser;
        private List<string> tweets;

        private ITwitAPI twitService;

        //Constructor
        public UserAnalysis(string userHandle, ITwitAPI twitService)
        {
            this.userHandle = userHandle.Substring(1);
            this.twitService = twitService;

            var waitTweets = this.twitService.GetTweets(this.userHandle);
            waitTweets.Wait();
            tweets = waitTweets.Result;
        }

        //Private Methods

        //Public Methods
        public string GetHandle()
        {
            return userHandle;
        }

        public List<string> getTweets()
        {
            return tweets;
        }

    }
}
