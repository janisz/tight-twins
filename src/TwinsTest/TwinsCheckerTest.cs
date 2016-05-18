// unit test code
using System;
using System.Collections.Generic;
using System.Linq;
using Twins.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Twins
{
    [TestClass]
    public class TwinsCheckerTest
    {
        [TestMethod]
        public void CheckTightTwins()
        {
            var twins1 = TwinsChecker.FindTightTwins(Sequence("312123"));
            Assert.IsNotNull(twins1);
            var twin1a = twins1.Item1.Select(x => x.Value).ToArray();
            var twin1b = twins1.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new [] { 1, 2 }, twin1a);
            CollectionAssert.AreEqual(new [] { 3, 4 }, twin1b);

            var twins2 = TwinsChecker.FindTightTwins(Sequence("231213231"));
            Assert.IsNotNull(twins2);
            var twin2a = twins2.Item1.Select(x => x.Value).ToArray();
            var twin2b = twins2.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new[] { 2, 3, 5 }, twin2a);
            CollectionAssert.AreEqual(new[] { 4, 6, 7 }, twin2b);

            var twins3 = TwinsChecker.FindTightTwins(Sequence("123132312"));
            Assert.IsNull(twins3);
        }

        public static ICollection<BoardItem> Sequence(string seq)
        {
            return seq.ToArray().Select((color, index) => new BoardItem() { Color = Convert.ToInt32(color.ToString()), Value = index }).ToList();
        }
    }
}