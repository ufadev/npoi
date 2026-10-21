/* ====================================================================
   SPDX-License-Identifier: Apache-2.0
==================================================================== */

namespace TestCases.Util
{
    using System;
    using System.Collections.Generic;
    using NPOI.Util.Collections;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    [TestFixture]
    public class TestStringTokenizer
    {
        private static List<string> Drain(StringTokenizer tokenizer)
        {
            var tokens = new List<string>();
            while (tokenizer.HasMoreTokens())
            {
                tokens.Add(tokenizer.NextToken());
            }
            return tokens;
        }

        [Test]
        public void DefaultDelimitersAreWhitespace()
        {
            var tokenizer = new StringTokenizer("  a\tbb\r\n ccc\f ");
            ClassicAssert.AreEqual(3, tokenizer.CountTokens());
            CollectionAssert.AreEqual(new[] { "a", "bb", "ccc" }, Drain(tokenizer));
            ClassicAssert.AreEqual(0, tokenizer.CountTokens());
        }

        [Test]
        public void CustomDelimitersCollapseRuns()
        {
            var tokenizer = new StringTokenizer(",,a,,b;c,", ",;");
            ClassicAssert.AreEqual(3, tokenizer.CountTokens());
            CollectionAssert.AreEqual(new[] { "a", "b", "c" }, Drain(tokenizer));
        }

        [Test]
        public void ReturnDelimitersYieldsEachDelimiterAsToken()
        {
            var tokenizer = new StringTokenizer("a,,b", ",", true);
            ClassicAssert.AreEqual(4, tokenizer.CountTokens());
            CollectionAssert.AreEqual(new[] { "a", ",", ",", "b" }, Drain(tokenizer));
        }

        [Test]
        public void CountTokensDoesNotAdvance()
        {
            var tokenizer = new StringTokenizer("x y z");
            ClassicAssert.AreEqual("x", tokenizer.NextToken());
            ClassicAssert.AreEqual(2, tokenizer.CountTokens());
            ClassicAssert.AreEqual(2, tokenizer.CountTokens());
            ClassicAssert.AreEqual("y", tokenizer.NextToken());
        }

        [Test]
        public void NextTokenWithDelimiterSwitchesDelimiterSet()
        {
            var tokenizer = new StringTokenizer("key=value pair", "=");
            ClassicAssert.AreEqual("key", tokenizer.NextToken());
            ClassicAssert.AreEqual("value", tokenizer.NextToken("= "));
            ClassicAssert.AreEqual("pair", tokenizer.NextToken());
            ClassicAssert.IsFalse(tokenizer.HasMoreTokens());
        }

        [Test]
        public void ExhaustedTokenizerThrows()
        {
            var tokenizer = new StringTokenizer("   ");
            ClassicAssert.IsFalse(tokenizer.HasMoreTokens());
            ClassicAssert.AreEqual(0, tokenizer.CountTokens());
            Assert.Throws<IndexOutOfRangeException>(() => tokenizer.NextToken());
        }

        [Test]
        public void EmptyInputHasNoTokens()
        {
            var tokenizer = new StringTokenizer(string.Empty, ",", true);
            ClassicAssert.IsFalse(tokenizer.HasMoreTokens());
            ClassicAssert.AreEqual(0, tokenizer.CountTokens());
        }
    }
}
