using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;

namespace UnitTestGameEngine
{
    [TestClass]
    public class GameEngineTests
    {
        Game ge = new Game();

        [TestMethod]
        public void Game_HasNoWinner()
        {
            var sut = new Game();
            sut.PlaceMark(0, 0);
            sut.PlaceMark(0, 1);
            sut.PlaceMark(1, 1);
            sut.PlaceMark(2, 2);
            sut.PlaceMark(0, 1);
            sut.PlaceMark(0, 2);
            sut.PlaceMark(1, 2);
            sut.PlaceMark(2, 1);
            sut.PlaceMark(0, 2);

            var result = sut.WhoIsWinner();
            Assert.AreEqual(Game.Mark.Nobody, result);
        }

        //public void GameHasWInner
        [TestMethod]
        public void ControlOfWhetherGameHasAWinnerOnColumn()
        {
            Game game = new Game();
            game.PlaceMark(0, 0);
            game.PlaceMark(1,1);
            game.PlaceMark(0, 1);
            game.PlaceMark(2, 2);
            game.PlaceMark(0, 2);
            var result = game.HasWinner();
            Assert.IsTrue(result);
        }

        public void ControlOfWhetherGameHasWinnerOnRow()
        {
            Game game = new Game();
            game.PlaceMark(0, 0);
            game.PlaceMark(0, 1);
            game.PlaceMark(1, 0);
            game.PlaceMark(0, 2);
            game.PlaceMark(2, 0);
            var result = game.HasWinner();
            Assert.IsTrue(result);
        }

        public void ControlOfWhetherGameHasWinnerOnColumn()
        {
            Game game = new Game();
            game.PlaceMark(0, 0);
            game.PlaceMark(2, 0);
            game.PlaceMark(0, 1);
            game.PlaceMark(1, 1);
            game.PlaceMark(2, 2);
            game.PlaceMark(0, 2);
            var result = game.HasWinner();
            Assert.IsTrue(result);
            Assert.AreEqual(game.GetMarkAt(0, 2), game.WhoIsWinner());           
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
            Game.Mark firstPlayer = game.CurrentPlayer;
            game.PlaceMark(1,1);
            Assert.AreNotEqual(game.CurrentPlayer, firstPlayer);
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
