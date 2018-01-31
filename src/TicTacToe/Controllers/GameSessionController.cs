﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Models;
using GameEngine;
using TicTacToe.Mail;

namespace TicTacToe.Controllers
{

    public class GameSessionController : Controller
    {
        private static Random idGenerator = new Random();
        private static Dictionary<int, GameSession> GameSessions = new Dictionary<int, GameSession>();
        private MailService mailService = new MailService();

        //Method that creates a session for a specific player, if the player has no session already.
        //Else the player is re-directed to her game in the web browser. 
        //Returns the first page: Lobby for Tic-tac-toe and a list of open game sessions if there are any.
        public ActionResult FirstPage()
        {
            Player currentPlayer = (Player)Session["player"];
            if (currentPlayer != null)
            {
                if (!GameSessions[currentPlayer.GameID].GameOver())
                {
                    return RedirectToBoard(currentPlayer.GameID);
                }
            }
            List<GameSession> openGames = new List<GameSession>();
            foreach (GameSession game in GameSessions.Values)
            {
                if (!game.GameFull)
                {
                    openGames.Add(game);
                }
            }
            return View("FirstPage", openGames);
        }

        public ActionResult JoinGame(string playerOName, int? id, string playerOEmail)
        {
            if (id != null)
            {
                GameSession game = GameSessions[(int)id];
                Player secondPlayer = new Player
                {
                    NickName = playerOName,
                    PlayerID = idGenerator.Next(),
                    MarkId = Game.Mark.PlayerO,
                    GameID = (int)id,
                    Email = playerOEmail
                };

                game.JoinGame(secondPlayer);
                game.StartGame();
                Session["player"] = secondPlayer;
                return RedirectToBoard((int)id);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult CreateGame(string playerXName, string playerXEmail)
        {
            System.Diagnostics.Debug.WriteLine("Creating game for player " + playerXName);
            int gameID = idGenerator.Next();
            GameSession newGame = new GameSession(gameID);
            GameSessions.Add(gameID, newGame);
            Player firstPlayer = new Player { NickName = playerXName, GameID = gameID, MarkId = Game.Mark.PlayerX, PlayerID = idGenerator.Next(), Email = playerXEmail };

            newGame.JoinGame(firstPlayer);
            Session["player"] = firstPlayer;
            return RedirectToBoard(gameID);
        }

        //To be able to look at a specific game we need to have an ID for the game as a parameter to the Actionresult.
        public ActionResult ShowGameBoard(int id)
        {
            if(!GameSessions.ContainsKey(id))
            {
                if(Session["player"] != null &&((Player)Session["player"]).GameID == id)
                {
                    Session.Remove("player");
                }
                return Redirect("/GameSession/GameNotFound");
            }
            GameSession game = GameSessions[id];
            System.Diagnostics.Debug.WriteLine("Showing the game board for game " + id);
            System.Diagnostics.Debug.WriteLine(game.SpecificGame.PrintGameBoard());
            return View("ShowGameBoard", game);
        }
        public ActionResult PlaceMark(int id, string coordinates)
        {
            GameSession game = GameSessions[id];

            if (((Player)Session["player"]).MarkId != game.SpecificGame.CurrentPlayer)
            {
                return RedirectToBoard(id);
            }
            if (!game.GameFull)
            {
                return RedirectToBoard(id);
            }
            System.Diagnostics.Debug.WriteLine("Placing mark at coordinates " + coordinates);
            string[] values = coordinates.Split(',');

            var isOk = game.SpecificGame.PlaceMark(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
            var playerList = game.PlayersInSpecificGame;
            var opponentPlayer = playerList.Where(player => player.MarkId != ((Player)Session["player"]).MarkId).FirstOrDefault();

            if (opponentPlayer != null && opponentPlayer.Email != "")
            {
                mailService.SendEmail(opponentPlayer.Email, opponentPlayer.NickName);
            }
            return RedirectToBoard(id);
        }

        private RedirectResult RedirectToBoard(int id)
        {
            return Redirect("/GameSession/ShowGameBoard/" + id.ToString());
        }

        public ActionResult GameNotFound()
        {
            return View("GameNotFound");
        }
    }
}