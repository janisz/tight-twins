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
            Assert.IsTrue(TwinsChecker.CheckTwins(Sequence("231213231")));
            Assert.IsTrue(TwinsChecker.CheckTwins(Sequence("123132312")));
        }
        
        public static ICollection<BoardItem> Sequence(string seq) {
            return seq.ToArray().Select(color => new BoardItem() { Color = Convert.ToInt32(color) }).ToList();
        }
    }
}