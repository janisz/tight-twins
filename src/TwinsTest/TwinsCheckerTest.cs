// unit test code
using System;
using System.Collections.Generic;
using System.Linq;
using Twins.Model;
using Twins.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Twins
{
    [TestClass]
    public class TwinsCheckerTest
    {
        [TestMethod]
        public void NormalizeSequence()
        {
            var sequence1 = Sequence("121");
            var sequence2 = Sequence("414");
            
            var normalized1 = TwinsChecker.SequenceToNormalizedString(sequence1);
            var normalized2 = TwinsChecker.SequenceToNormalizedString(sequence2);

            Assert.AreEqual("0 1 0", normalized1);
            Assert.AreEqual("0 1 0", normalized2);
        }

        [TestMethod]
        public void CheckTightTwins()
        {
            var sequence1 = Sequence("312123");
            var twins1 = TwinsChecker.FindTightTwins(sequence1);
            var areTwins1 = TwinsChecker.CheckTwins(sequence1);
            Assert.IsTrue(areTwins1);
            var twin1a = twins1.Item1.Select(x => x.Value).ToArray();
            var twin1b = twins1.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new[] { 1, 2 }, twin1a);
            CollectionAssert.AreEqual(new[] { 3, 4 }, twin1b);

            var sequence2 = Sequence("231213231");
            var twins2 = TwinsChecker.FindTightTwins(sequence2);
            var areTwins2 = TwinsChecker.CheckTwins(sequence2);
            Assert.IsTrue(areTwins2);
            var twin2a = twins2.Item1.Select(x => x.Value).ToArray();
            var twin2b = twins2.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new[] { 2, 3, 5 }, twin2a);
            CollectionAssert.AreEqual(new[] { 4, 6, 7 }, twin2b);

            var sequence3 = Sequence("123132312");
            var twins3 = TwinsChecker.FindTightTwins(sequence3);
            var areTwins3 = TwinsChecker.CheckTwins(sequence3);
            Assert.IsFalse(areTwins3);

            var sequence4 = Sequence("11");
            var twins4 = TwinsChecker.FindTightTwins(sequence4);
            var areTwins4 = TwinsChecker.CheckTwins(sequence4);
            Assert.IsTrue(areTwins4);
            var twin4a = twins4.Item1.Select(x => x.Value).ToArray();
            var twin4b = twins4.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new[] { 0 }, twin4a);
            CollectionAssert.AreEqual(new[] { 1 }, twin4b);
        }

        [TestMethod]
        public void CheckBeterFirstPlayerMove()
        {
            //TODO: Test interface not helper methods
            var move = BetterFirstPlayer.MinMove(Sequence("02012"), 3, 7);
            Assert.AreEqual(1, move.position);
            move = BetterFirstPlayer.MinMove(Sequence("01202"), 3, 7);
            Assert.AreEqual(2, move.position);
        }

        public static List<BoardItem> Sequence(string seq)
        {
            return seq.ToArray().Select((color, index) => new BoardItem() { Color = Convert.ToInt32(color.ToString()), Value = index }).ToList();
        }
    }
}