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
        public void CheckTightTwins()
        {
            var twins1 = TwinsChecker.FindTightTwins(Sequence("312123"));
            Assert.IsNotNull(twins1);
            var twin1a = twins1.Item1.Select(x => x.Value).ToArray();
            var twin1b = twins1.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new[] { 1, 2 }, twin1a);
            CollectionAssert.AreEqual(new[] { 3, 4 }, twin1b);

            var twins2 = TwinsChecker.FindTightTwins(Sequence("231213231"));
            Assert.IsNotNull(twins2);
            var twin2a = twins2.Item1.Select(x => x.Value).ToArray();
            var twin2b = twins2.Item2.Select(x => x.Value).ToArray();
            CollectionAssert.AreEqual(new[] { 2, 3, 5 }, twin2a);
            CollectionAssert.AreEqual(new[] { 4, 6, 7 }, twin2b);

            var twins3 = TwinsChecker.FindTightTwins(Sequence("123132312"));
            Assert.IsNull(twins3);

            var twins4 = TwinsChecker.FindTightTwins(Sequence("11"));
            Assert.IsNotNull(twins4);
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
            Assert.AreEqual(4, move.position);
        }
        

        public static List<BoardItem> Sequence(string seq)
        {
            return seq.ToArray().Select((color, index) => new BoardItem() { Color = Convert.ToInt32(color.ToString()), Value = index }).ToList();
        }
    }
}