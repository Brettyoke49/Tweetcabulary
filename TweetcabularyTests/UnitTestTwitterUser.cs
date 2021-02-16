using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tweetcabulary.Models;

namespace TweetcabularyTests
{
    [TestClass]
    public class UnitTestTwitterUser
    {
        private Tweet tweet1;
        private Tweet tweet2;
        private Tweet tweet3;

        public UnitTestTwitterUser()
        {
            List<string> hashtags1 = new List<string> { "#cool" };
            tweet1 = new Tweet("Wow", hashtags1, "testUser");
            List<string> hashtags2 = new List<string> { "#neat" };
            tweet2 = new Tweet("", hashtags1, "testUser");
            List<string> hashtags3 = new List<string> { };
            tweet3 = new Tweet("What an unordinarily verbose tweet", hashtags3, "testUser");
        }

        [TestMethod]
        public void TestGetAllTweetText()
        {
            List<Tweet> tweets = new List<Tweet> { tweet1, tweet2, tweet3 };
            TwitterUser user1 = new TwitterUser(tweets, "testUser");
            List<Tweet> tweets2 = new List<Tweet> { tweet1, tweet2 };
            TwitterUser user2 = new TwitterUser(tweets2, "testUser");
            List<Tweet> tweets3 = new List<Tweet> { tweet2 };
            TwitterUser user3 = new TwitterUser(tweets3, "testUser");

            List<string> ans1 = new List<string> { "Wow", "", "What an unordinarily verbose tweet" };
            List<string> ans2 = new List<string> { "Wow", "" };
            List<string> ans3 = new List<string> { "" };

            List<string> ans1Actual = user1.GetAllTweetText();
            CollectionAssert.AreEqual(ans1, user1.GetAllTweetText(), "Incorrect tweet text returned");
            CollectionAssert.AreEqual(ans2, user2.GetAllTweetText(), "Incorrect tweet text returned");
            CollectionAssert.AreEqual(ans3, user3.GetAllTweetText(), "Incorrect tweet text returned");
        }

        [TestMethod]
        public void TestIsValidUser()
        {
            List<Tweet> tweets = new List<Tweet> { tweet1, tweet2, tweet3 };
            TwitterUser user1 = new TwitterUser(tweets, "testUser");
            TwitterUser user2 = new TwitterUser(false, "testUser");
            List<Tweet> tweets3 = new List<Tweet> { tweet2 };
            TwitterUser user3 = new TwitterUser(tweets3, "testUser");

            Assert.IsTrue(user1.IsValidUser());
            Assert.IsFalse(user2.IsValidUser());
            Assert.IsFalse(user3.IsValidUser());
        }

        [TestMethod]
        public void TestGetTweetCount()
        {
            List<Tweet> tweets = new List<Tweet> { tweet1, tweet2, tweet3 };
            TwitterUser user1 = new TwitterUser(tweets, "testUser");
            List<Tweet> tweets2 = new List<Tweet> { tweet2 };
            TwitterUser user2 = new TwitterUser(tweets2, "testUser");
            List<Tweet> tweets3 = new List<Tweet> {  };
            TwitterUser user3 = new TwitterUser(tweets3, "testUser");

            Assert.AreEqual(3, user1.GetTweetCount());
            Assert.AreEqual(1, user2.GetTweetCount());
            Assert.AreEqual(0, user3.GetTweetCount());
        }
    }
}
