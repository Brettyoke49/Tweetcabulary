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

        //Constructor
        public UserAnalysis(string userHandle, ITwitAPI twitService)
        {
            //Forcing user to enter @ to make clear what name is needed, but we don't actually want it
            if (userHandle[0] == '@') userHandle.Substring(1);

            this.twitService = twitService;
            var waitTweets = this.twitService.GetUserTweets(userHandle);
            waitTweets.Wait();
            twitterUser = waitTweets.Result;

            if(!twitterUser.IsValidUser())
            {

            }
        }

        //Private Methods

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

    }
}
