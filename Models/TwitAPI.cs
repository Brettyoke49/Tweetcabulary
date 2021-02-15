using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Tweetcabulary.Models
{
    public interface ITwitAPI
    {
        void Authenticate();
        Task<List<string>> GetTweets(string userHandle);
    }

    public class TwitAPI : ITwitAPI
    {
        private static string APIKey = "XRFfi6gtf61i30l5AIQ9ikgBv";
        private static string APISecret = "fo0OZsJ94CKIMFqVKTro0hDKGoe74B5UE0F28NujWnr6wnYUJx";
        private static string Bearer = "AAAAAAAAAAAAAAAAAAAAAGrPMgEAAAAAn3jlv0JGjrWmPfaprpwbrkm6MFw%3DvqrB12jhSiEFhfJeFnzUARVuRJhURXHIWlSM7PKQ2G6kV6shB0";

        private Tweetinvi.TwitterClient appClient;

        public TwitAPI() {
            appClient = new TwitterClient(APIKey, APISecret, Bearer);
        }

        public void Authenticate(){ }

        public async Task<List<string>> GetTweets(string userHandle)
        {
            List<string> tweets = new List<string>();
            var userTweets = await appClient.Timelines.GetUserTimelineAsync(userHandle);
            foreach (var tweet in userTweets)
            {
                tweets.Add(tweet.Text);
            }

            return tweets;
        }
    }
}
