using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tweetcabulary.Models;

namespace TweetcabularyTests
{
    [TestClass]
    public class UnitTestTweet
    {
        [TestMethod]
        public void TestSyllableCount()
        {
            List<string> hashtags1 = new List<string> { "#cool" };
            Tweet tweet1 = new Tweet("Wow that is pretty darn wicked", hashtags1, "testUser");
            List<string> hashtags2 = new List<string> { "#neat" };
            Tweet tweet2 = new Tweet("", hashtags1, "testUser");

            Assert.AreEqual(8, tweet1.GetSyllableCount(), "Incorrect syllable count");
            Assert.AreEqual(0, tweet2.GetSyllableCount(), "Incorrect syllable count");
        }

        [TestMethod]
        public void TestCharacterCount()
        {
            List<string> hashtags1 = new List<string> { "#cool" };
            Tweet tweet1 = new Tweet("Wow that is pretty darn wicked", hashtags1, "testUser");
            List<string> hashtags2 = new List<string> { "#neat" };
            Tweet tweet2 = new Tweet("", hashtags1, "testUser");

            Assert.AreEqual(25, tweet1.GetCharacterCount(), "Incorrect syllable count");
            Assert.AreEqual(0, tweet2.GetCharacterCount(), "Incorrect syllable count");
        }
    }
}
