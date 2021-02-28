using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweetinvi.Models.Entities;
using Tweetinvi.Iterators;

namespace Tweetcabulary.Models
{
    public interface ITwitAPI
    {
        void Authenticate(string key1, string key2, string key3);
        Task<TwitterUser> GetUserTweets(string userHandle);
    }

    public class TwitAPI : ITwitAPI
    {
        //Member variables
        private Tweetinvi.TwitterClient appClient;


        //Constructor
        public TwitAPI() {
        }

        //Call at startup to force initialization and authentication of this singleton
        public void Authenticate(string APIKey, string APISecret, string Bearer){
            appClient = new TwitterClient(APIKey, APISecret, Bearer);
        }

        //Private Methods
        private string ParseTweet(string tweet)
        {
            tweet = Regex.Replace(tweet, "’s", "");
            //Remove hashtags, user tags, 's contractions/possessions
            tweet = Regex.Replace(tweet, @"[\@\#]\S*|['’]s\S*", "");

            //Remove links
            tweet = Regex.Replace(tweet, @"http\S*|www\.\S*", "");

            //Remove non-alpha, apostrophe, or space characters
            tweet = Regex.Replace(tweet, @"[^a-zA-Z '’]", " ");

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
        /// <returns>TwitterUser object containing up to 1500 most recent tweets</returns>
        public async Task<TwitterUser> GetUserTweets(string userHandle)
        {
            IUser user;
            try
            {
                user = await appClient.Users.GetUserAsync(userHandle);
            }
            catch (Exception)
            {
                return new TwitterUser(false, userHandle);
            }
            
            List<Tweet> tweets = new List<Tweet>();
            ITwitterIterator<ITweet, long?> userTimelineIterator = appClient.Timelines.GetUserTimelineIterator(user.Id);

            while (!userTimelineIterator.Completed && tweets.Count <= 1500)
            {
                var page = await userTimelineIterator.NextPageAsync();

                foreach (var tweet in page)
                {
                    if (tweets.Count >= 1500) break;
                    if (String.Equals(tweet.CreatedBy.ScreenName.ToLower(), userHandle) && !tweet.IsRetweet)
                    {
                        string tweetText = ParseTweet(tweet.Text);
                        List<string> hashtags = ParseHashtags(tweet.Hashtags);

                        tweets.Add(new Tweet(tweetText, hashtags, userHandle));
                    }
                }

            }

            return new TwitterUser(tweets, userHandle);
        }
    }
}
