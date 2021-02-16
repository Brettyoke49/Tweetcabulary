using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Tweetcabulary.Models;


namespace TweetcabularyTests
{
    [TestClass]
    public class UnitTestSpellCheck
    {
        [TestMethod]
        public void TestIsValidWord()
        {
            SpellCheck spellChecker = new SpellCheck();
            spellChecker.loadWords(@"C:\Users\brett\source\repos\Tweetcabulary\Tweetcabulary\English.dic");

            Assert.IsTrue(spellChecker.IsValidWord("toast"));
            Assert.IsTrue(spellChecker.IsValidWord("platitudinous"));
            Assert.IsTrue(spellChecker.IsValidWord("Platitudinous"));
            Assert.IsTrue(spellChecker.IsValidWord("xylophone"));
            Assert.IsFalse(spellChecker.IsValidWord(""));
            Assert.IsFalse(spellChecker.IsValidWord(" "));
            Assert.IsFalse(spellChecker.IsValidWord("fakeword"));
            Assert.IsFalse(spellChecker.IsValidWord("platitoodinous"));
            Assert.IsFalse(spellChecker.IsValidWord("toastt"));
        }
    }
}
