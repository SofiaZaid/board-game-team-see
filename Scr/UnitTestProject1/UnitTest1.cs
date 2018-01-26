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

        [TestMethod]
        public void Game_GetMarkAt()
        {
            var sut = new Game();
            sut.gameBoard[1, 1] = Game.Mark.PlayerO;
            var result = sut.GetMarkAt(1, 1);
            Assert.AreEqual(Game.Mark.PlayerO,result);
                
        }

        [TestMethod]
        public void Game_WinnerOnColumn()
        {
            var sut = new Game();
            var result = sut.WinnerOnRow(1);
            Assert.AreEqual(Game.Mark.Nobody, result);
        }


        [TestMethod]
        public void Game_isFree()
        {
            var sut = new Game();
            var result = sut.IsFree(1, 1);

            Assert.IsTrue(result);
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
            Assert.AreEqual(newcurrentPlayer, Game.Mark.PlayerO);
            Assert.AreEqual(currentPlayer, Game.Mark.PlayerX);
        }

        //Method that tests methods PlaceMark and GetMarkAt to see that after we have placed a mark on the gameboard 
        //playerbefore is not the currentplayer and that the mark we now see placed on the board belongs to playerbefore.
        [TestMethod]
        public void CheckThatCorrectMarkIsReturnedDependingOnXAndYCoordinates()
        {
            Game game = new Game();
            Game.Mark playerBefore = game.CurrentPlayer;
            game.PlaceMark(2, 1);
            Assert.AreNotEqual(playerBefore, game.CurrentPlayer);
            Assert.AreEqual(playerBefore, game.GetMarkAt(2, 1));

        }

        




    }
}
