using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;

namespace UnitTestProject1
{
    [TestClass]
    public class GameEngineTests
    {
        Game ge = new Game();

        [TestMethod]
        public void TestMethod1()
        {
            Game game = new Game();
            var result = game.XPlusY(1,2);
            Assert.AreEqual(3,result);

        }
    }
}
