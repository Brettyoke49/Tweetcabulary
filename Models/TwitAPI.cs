using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweetinvi.Models.Entities;

namespace Tweetcabulary.Models
{
    public interface ITwitAPI
    {
        void Authenticate();
        Task<TwitterUser> GetUserTweets(string userHandle);
    }

    public class TwitAPI : ITwitAPI
    {
        //API Keys
        private static string APIKey = "XRFfi6gtf61i30l5AIQ9ikgBv";
        private static string APISecret = "fo0OZsJ94CKIMFqVKTro0hDKGoe74B5UE0F28NujWnr6wnYUJx";
        private static string Bearer = "AAAAAAAAAAAAAAAAAAAAAGrPMgEAAAAAn3jlv0JGjrWmPfaprpwbrkm6MFw%3DvqrB12jhSiEFhfJeFnzUARVuRJhURXHIWlSM7PKQ2G6kV6shB0";

        //Member variables
        private Tweetinvi.TwitterClient appClient;

        //Constructor
        public TwitAPI() {
            appClient = new TwitterClient(APIKey, APISecret, Bearer);
        }

        //Call at startup to force initialization and authentication of this singleton
        public void Authenticate(){ }

        //Private Methods
        private string ParseTweet(string tweet)
        {
            //Remove hashtags, user tags, emoticons, and other invalid words from tweets
            tweet = Regex.Replace(tweet, @"[\@\#\:\;\(\)]\w+", "");

            //Remove non-alpha or space characters
            tweet = Regex.Replace(tweet, @"[^a-zA-Z ]", "");

            return tweet;
        }

        private List<string> ParseHashtags(in List<IHashtagEntity> hashtags)
        {
            List<string> hashtagList = new List<string>();

            foreach(var h in hashtags)
            {
                hashtagList.Add(h.Text);
            }

            return hashtagList;
        }

        //Public Methods

        /// <summary>
        /// Get all tweets for a given user
        /// </summary>
        /// <param name="userHandle">Username for twitter account</param>
        /// <returns>TwitterUser object containing up to 3200 most recent tweets</returns>
        public async Task<TwitterUser> GetUserTweets(string userHandle)
        {
            ITweet[] userTweets = new ITweet[] { };
            List<Tweet> tweets = new List<Tweet>();

            try
            {
                userTweets = await appClient.Timelines.GetUserTimelineAsync(userHandle);
            } 
            catch (Exception ex)
            {
                //Error 34 indicates invalid user
                if(ex.Message.Contains("34"))
                {
                    //Return empty list if invalid user.
                    return new TwitterUser(false, userHandle);
                }
                else
                {
                    throw new Exception("Error obtaining user: " + userHandle + ", " + ex.Message);
                }
            }

            //Create Tweet objects to pass to user
            foreach (var tweet in userTweets)
            {
                //Remove hashtags and user tags from tweet text
                string tweetText = ParseTweet(tweet.Text);
                List<string> hashtags = ParseHashtags(tweet.Hashtags);

                tweets.Add(new Tweet(tweetText, hashtags, userHandle));
            }

            return new TwitterUser(tweets, userHandle);
        }
    }
}
