﻿using System;
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

        //TestMethod that tests so that currentPlayer from the beginning is player X and so that the currentPlayer value is changed 
        //into PlayerO when method ChangePlayerTurn is called.
        [TestMethod]
        public void CorrectlyChangingPlayerTurn()
        {
            Game game = new Game();
            Game.Mark currentPlayer = game.CurrentPlayer;
            game.ChangePlayerTurn();
            Game.Mark newcurrentPlayer = game.CurrentPlayer;
            Assert.Equals(newcurrentPlayer, Game.Mark.PlayerO);
            Assert.Equals(currentPlayer, Game.Mark.PlayerX);
        }

    }
}
