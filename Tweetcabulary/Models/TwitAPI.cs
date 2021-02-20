﻿using System;
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
        /// <returns>TwitterUser object containing up to 3200 most recent tweets</returns>
        public async Task<TwitterUser> GetUserTweets(string userHandle)
        {
            List<Tweet> tweets = new List<Tweet>();
            ITwitterIterator<ITweet, long?> userTimelineIterator;
            try
            {
                userTimelineIterator = appClient.Timelines.GetUserTimelineIterator(userHandle);
            }
            catch (Exception ex)
            {
                //Error 34 indicates invalid user, 401 indicates private user (authorization required)
                if (ex.Message.Contains("34") || ex.Message.Contains("401"))
                {
                    //Return invalid user
                    return new TwitterUser(false, userHandle);
                }
                else
                {
                    throw new Exception("Error obtaining user: " + userHandle + ", " + ex.Message);
                }
            }

            while (!userTimelineIterator.Completed)
            {
                var page = await userTimelineIterator.NextPageAsync();
                foreach(var tweet in page) {
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
